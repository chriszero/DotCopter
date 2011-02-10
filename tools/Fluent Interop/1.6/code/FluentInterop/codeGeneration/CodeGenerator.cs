using System;
using System.Collections;
using FluentInterop.CodeGeneration.Entities;
using FluentInterop.CodeGeneration.Util;
using FluentInterop.Coding;
using FluentInterop.Expressions.StaticFunctions;
using FluentInterop.OpCodes;
using FluentInterop.Representations;
using Kosak.SimpleInterop;
using Microsoft.SPOT;

namespace FluentInterop.CodeGeneration {
  public sealed partial class CodeGenerator {
    public static CompiledCode Compile(ActionOnCodeGenerator build) {
      var hints=new CodeGeneratorHints();
      var phaseIndex=0;
      while(true) {
        try {
          var result=new CodeGenerator(hints, build, phaseIndex++).Finish();
          hints=result as CodeGeneratorHints;
          if(hints==null) {
            return (CompiledCode)result;
          }
        } finally {
          Instance=null;
        }
      }
    }

    public static CodeGenerator Instance { get; private set; }
    public static Emitter Emitter { get { return Instance.emitter; } }

    public readonly CodeGeneratorHints GeneratorHints;

    private readonly Emitter emitter=new Emitter();

    public readonly Namespace TopNamespace=new Namespace(null, 0, "top");

    private bool labelsHaveChanged;
    /// <summary>
    /// Hashtable&lt;TerminalName,int&gt;
    /// </summary>
    private readonly Hashtable labelNameToAddress;
    /// <summary>
    /// ArrayList&lt;FuncDefinition&gt;
    /// </summary>
    private readonly ArrayList funcDefinitions=new ArrayList();
    /// <summary>
    /// Hashtable&lt;IReference,StaticVariableInfo&gt;
    /// </summary>
    private readonly Hashtable staticVariableToInfo=new Hashtable();
    /// <summary>
    /// </summary>
    private readonly Label[] registerIndexToLabelWhereItsBXInstructionIs=new Label[Traits.NumLowRegisters];

    private CodeGenerator(CodeGeneratorHints generatorHints, ActionOnCodeGenerator action, int phase) {
      Instance=this;
      this.GeneratorHints=generatorHints;
      this.labelNameToAddress=generatorHints.LabelNameToAddress.ToHashtable();

      var stars=new string('*', 78);
      Debug.Print(stars);
      Debug.Print(stars);
      Debug.Print("***** STARTING PHASE "+phase);
      Debug.Print(stars);
      Debug.Print(stars);

      action(this);
    }

    private object Finish() {
      bool runAgain;
      var newFbHints=EmitStaticFunctions(out runAgain);
      EmitBXInstructions();
      EmitStatics();

      return labelsHaveChanged || runAgain
        ? (object)GeneratorHints.CloneSetValues(labelNameToAddress.AsReadOnlyDictionary(), newFbHints.AsReadOnlyDictionary())
        : new CompiledCode(emitter.MakeOpCodes());
    }

    private Hashtable EmitStaticFunctions(out bool runAgain) {
      runAgain=false;
      var hintsForNextTime=new Hashtable(); //Hashtable<string,FuncBuilderHints>
      for(var i=0; i<funcDefinitions.Count; ++i) {
        emitter.AlignToWord();
        var definition=(FuncDefinition)funcDefinitions[i];
        var declaration=definition.Declaration;
        var name=declaration.Name;
        var fbHints=(FuncBuilderHints)GeneratorHints.FuncNameToFuncBuilderHints[name] ?? new FuncBuilderHints();

        var ns=new Namespace(this.TopNamespace, i, name);
        declaration.EntryPoint.Mark();
        bool fbRunAgain;
        var newFbHints=FuncBuilder.EmitFunction(ns, fbHints, definition.Body, out fbRunAgain);
        hintsForNextTime.Add(name, newFbHints);
        runAgain|=fbRunAgain;
      }
      return hintsForNextTime;
    }

    private void EmitBXInstructions() {
      for(var registerIndex=0; registerIndex<registerIndexToLabelWhereItsBXInstructionIs.Length; ++registerIndex) {
        var label=registerIndexToLabelWhereItsBXInstructionIs[registerIndex];
        if(label!=null) {
          label.Mark();
          var register=new LowRegister(registerIndex);
          emitter.Emit(Format5OpCode.BX, null, register);
        }
      }
    }

    private void EmitStatics() {
      //In order to be slightly nicer to our callers, we emit statics in order of declaration.
      var staticVariables=new Static[staticVariableToInfo.Count];
      foreach(StaticVariableInfo vi in staticVariableToInfo.Values) {
        var staticVar=vi.Representation;
        staticVariables[staticVar.DeclarationIndex]=staticVar;
      }

      foreach(var staticVar in staticVariables) {
        emitter.AlignToWord();
        var label=staticVar.StorageLabel;
        label.Mark();
        var labelName=label.Name.ToString();
        var initialData=staticVar.InitialData;

        for(var i=0; i<initialData.Length; ++i) {
          var item=initialData[i];
          var lo=(short)(item&0xffff);
          var hi=(short)(item>>16);

          var loComment="lo("+item.ToHex()+")";
          var hiComment="hi("+item.ToHex()+")";

          var assemblerComment="word "+i+" of "+labelName;

          emitter.Emit(lo, loComment, assemblerComment);
          emitter.Emit(hi, hiComment, assemblerComment);
        }
      }
    }

    public Label LookupOrCreateBranchTo(LowRegister register) {
      var regIndex=register.Index;
      var label=registerIndexToLabelWhereItsBXInstructionIs[regIndex];
      if(label==null) {
        label=DeclareStaticLabel("bxTo"+register);
        registerIndexToLabelWhereItsBXInstructionIs[regIndex]=label;
      }
      return label;
    }

    public StaticVariableInfo GetStaticVariableInfo(IReference variable, bool strict) {
      var result=(StaticVariableInfo)this.staticVariableToInfo[variable];
      if(strict && result==null) {
        throw new Exception("variable not found");
      }
      return result;
    }

    public void BindStaticVariableToNewRepresentation(IReference variable, string name, int[] initialData) {
      var terminalName=CreateTerminalName(name);
      var label=new Label(terminalName);
      var representation=new Static(staticVariableToInfo.Count, label, initialData);
      staticVariableToInfo.CheckedAdd(variable, new StaticVariableInfo(terminalName, representation));
    }

    private TerminalName CreateTerminalName(string name) {
      return new TerminalName(TopNamespace, name);
    }

    public Label DeclareStaticLabel(string name) {
      return new Label(CreateTerminalName(name));
    }

    /// <summary>
    /// Mark the point in the generated code which is this label's target
    /// </summary>
    public void MarkLabel(TerminalName name) {
      var currentAddress=emitter.CurrentAddress;
      var previousAddress=labelNameToAddress[name];
      if(previousAddress!=null && (int)previousAddress!=currentAddress) {
        labelsHaveChanged=true;
      }
      labelNameToAddress[name]=currentAddress;
      Debug.Print(name+":");
    }

    /// <summary>
    /// These label addresses aren't guaranteed correct until the final pass of the compiler
    /// </summary>
    public int GetLabelAddressBestEffort(TerminalName targetName) {
      var result=labelNameToAddress[targetName];
      if(result==null) {
        labelsHaveChanged=true;
        return 0;
      }
      return (int)result;
    }

    public Label AllocateInlineConstant(int constant) {
      //we do linear search which is unfortunate, but we are trading off time for memory here
      var constName="const("+constant+")";
      var terminalName=CreateTerminalName(constName);
      foreach(StaticVariableInfo vi in staticVariableToInfo.Values) {
        if(vi.Name.Equals(terminalName)) {
          return vi.Representation.StorageLabel;
        }
      }
      var variable=Declare.Static.Int(constName, constant);
      return GetStaticVariableInfo(variable, true).Representation.StorageLabel;
    }

    public void AddFuncDefinition(FuncDefinition definition) {
      funcDefinitions.Add(definition);
    }
  }
}
