using System;
using System.Collections;
using FluentInterop.CodeGeneration.Entities;
using FluentInterop.CodeGeneration.RegisterAllocation;
using FluentInterop.CodeGeneration.Util;
using FluentInterop.Coding;
using FluentInterop.OpCodes;
using FluentInterop.Representations;
using Microsoft.SPOT;

namespace FluentInterop.CodeGeneration {
  public partial class FuncBuilder {
    public static FuncBuilderHints EmitFunction(Namespace ns, FuncBuilderHints fbHints, ActionOnFuncBuilder action, out bool runAgain) {
      try {
        //HACK
        var emitter=CodeGenerator.Emitter;
        emitter.RegistersWritten=0;
        var fb=new FuncBuilder(ns, fbHints, action);
        var rw=emitter.RegistersWritten;
        var scratch0Used=(rw&(1<<fb.Scratch0.Index))!=0;
        var scratch1Used=(rw&(1<<fb.Scratch1.Index))!=0;
        return fb.RunOptimizer(scratch0Used, scratch1Used, out runAgain);
      } finally {
        Instance=null;
      }
    }

    public static FuncBuilder Instance { get; private set; }

    private NamespaceAndNextIndex namespaceAndNextIndex;
    public readonly FuncBuilderHints BuilderHints;

    public readonly LowRegister Scratch0;
    public readonly LowRegister Scratch1;

    public int StackPointer { get; private set; }
    public readonly int EndOfVariableRegion;

    public readonly Label TheExitLabel;

    /// <summary>
    /// We use this variable on the first pass only, in order to put newly-discovered variables *somewhere*
    /// </summary>
    private int simpleAllocatorIndexForPass0;

    private bool externalMethodWasInvoked;

    /// <summary>
    /// linked list of scopes, from inner to outer, storing our active variables
    /// </summary>
    private VariableToInfo localVariableToInfo=new VariableToInfo();
    /// <summary>
    /// All the variables in this function (not just the current scope)
    /// Hashtable&lt;TerminalName,dontcare&gt;
    /// </summary>
    private readonly Hashtable liveLocalVariableNames=new Hashtable();
    /// <summary>
    /// Variables which have a "pinned" representation (at the moment this holds true only for parameters)
    /// Hashtable&lt;TerminalName,Representation&gt;
    /// </summary>
    private readonly Hashtable pinnedVariables=new Hashtable();
    /// <summary>
    /// This contains a little map of fluent utterances that are in-flight but not complete.
    /// This allows us to issue an error message, for example, if our client uses an "If"
    /// without the corresponding  "EndIf". In the steady state this map is empty.
    /// Hashtable&lt;object,string&gt;
    /// </summary>
    private readonly Hashtable inflightUtterances=new Hashtable();

    private FuncBuilder(Namespace ns, FuncBuilderHints builderHints, ActionOnFuncBuilder action) {
      Instance=this;
      this.namespaceAndNextIndex=new NamespaceAndNextIndex(ns, 0);
      this.BuilderHints=builderHints;

      var pt=builderHints.PassTraits;
      this.Scratch0=pt.Scratch0;
      this.Scratch1=pt.Scratch1;

      var emitter=CodeGenerator.Emitter;

      var passTraits=BuilderHints.PassTraits;
      var preserveLr=passTraits.PreserveLinkRegister;
      var regsToPreserve=passTraits.RegisterMask&0xf0;
      emitter.EmitIfNecessary(Format14OpCode.PUSH, preserveLr, (byte)regsToPreserve);
      if(preserveLr) {
        StackPointer+=-4;
      }
      while(regsToPreserve!=0) {
        StackPointer+=-4;
        regsToPreserve&=(regsToPreserve-1);
      }
      this.EndOfVariableRegion=StackPointer;

      var avo=passTraits.AllocatedVariableOffset;
      emitter.EmitIfNecessary(Format13OpCode.ADDSP, avo);
      StackPointer+=avo;

      this.TheExitLabel=DeclareLabel("leave");

      action(this);

      TheExitLabel.Mark();
      StackPointer+=-avo;
      emitter.EmitIfNecessary(Format13OpCode.ADDSP, -avo);

      regsToPreserve=passTraits.RegisterMask&0xf0;
      emitter.EmitIfNecessary(Format14OpCode.POP, false, (byte)regsToPreserve);

      Register bxRegister;
      if(passTraits.PreserveLinkRegister) {
        bxRegister=new LowRegister(1);
        emitter.EmitIfNecessary(Format14OpCode.POP, false, 1<<1);
      } else {
        bxRegister=Register.LR;
      }
      emitter.Emit(Format5OpCode.BX, null, bxRegister);

      if(inflightUtterances.Count>0) {
        var allUtterances="";
        foreach(string utterance in inflightUtterances.Values) {
          if(allUtterances.Length>0) {
            allUtterances+=",";
          }
          allUtterances+=utterance;
        }
        throw new Exception(allUtterances);
      }
    }

    public FuncBuilderHints RunOptimizer(bool scratch0Used, bool scratch1Used, out bool runAgain) {
      var liveLocalNonParameterVariableNames=new Hashtable();
      var usedRegisterMask=0;
      foreach(TerminalName variableName in liveLocalVariableNames.Keys) {
        var rep=(Representation)pinnedVariables[variableName];
        if(rep==null) {
          liveLocalNonParameterVariableNames.Add(variableName, null);
        } else {
          var register=rep as Register;
          if(register!=null) {
            usedRegisterMask|=1<<register.Index;
          }
        }
      }
      var allocableRegisterMask=((1<<Traits.NumAllocableRegisters)-1)&~usedRegisterMask;

      var result=VariableAllocator.Run(liveLocalNonParameterVariableNames.Keys,
        allocableRegisterMask,
        BuilderHints.VariableNameToRepresentation);

      var allUsedExcludingScratch=usedRegisterMask|result.UsedRegisterMask;

      //The reason for this is that scratch0 and scratch1 must not interfere with the calling convention
      var startingPoint=externalMethodWasInvoked ? 4 : 0;
      LowRegister scratch0=null;
      for(var i=startingPoint; i<Traits.NumLowRegisters; ++i) {
        if((allUsedExcludingScratch&(1<<i))==0) {
          var register=new LowRegister(i);
          if(scratch0==null) {
            scratch0=register;
          } else {
            var pt=new PassTraits(this.externalMethodWasInvoked, scratch0, register,
              scratch0Used, scratch1Used, allUsedExcludingScratch, result.AllocatedVariableOffset);
            runAgain=result.RunAgain || !pt.EqualTo(BuilderHints.PassTraits);
            return BuilderHints.CloneSetValues(liveLocalVariableNames.AsReadOnlyDictionary(),
              result.VariableNameToRepresentation, pt);
          }
        }
      }
      throw new Exception("never");
    }

    public IDisposable OpenScope(string name) {
      this.namespaceAndNextIndex=this.namespaceAndNextIndex.Nest(name);
      this.localVariableToInfo=this.localVariableToInfo.Nest();
      return new ScopeMaster();
    }

    private class ScopeMaster : IDisposable {
      public void Dispose() {
        var f=FuncBuilder.Instance;
        f.namespaceAndNextIndex=f.namespaceAndNextIndex.UnNest();
        f.localVariableToInfo=f.localVariableToInfo.UnNest();
      }
    }

    public TerminalName CreateTerminalName(string name) {
      return new TerminalName(namespaceAndNextIndex.Namespace, name);
    }

    public Label DeclareLabel(string name) {
      return new Label(CreateTerminalName(name));
    }

    public VariableInfo GetStaticOrLocalVariableInfo(IReference variable, bool strict) {
      return (VariableInfo)CodeGenerator.Instance.GetStaticVariableInfo(variable, false)
        ?? GetLocalVariableInfo(variable, strict);
    }

    public LocalVariableInfo GetLocalVariableInfo(IReference variable, bool strict) {
      return localVariableToInfo.LookupExists(variable, strict);
    }

    public void BindLocalVariableToRepresentation(IReference variable, string name, Representation representation=null) {
      var terminalName=CreateTerminalName(name);
      if(representation!=null) {
        pinnedVariables[terminalName]=representation;
      }
      if(representation==null && !BuilderHints.TryGetRepresentation(terminalName, out representation)) {
        if(CodeGenerator.Instance.GeneratorHints.IsFirstPass) {
          simpleAllocatorIndexForPass0-=4;
          representation=new StackWordRelativeToEndOfVariables(simpleAllocatorIndexForPass0);
        }
        //else, representation is null
      }
      localVariableToInfo.Add(variable, new LocalVariableInfo(terminalName, representation));
    }

    public bool IsLive(IReference reference) {
      return liveLocalVariableNames.Contains(reference.GetName());
    }

    public void NoteRead(IReference reference) {
      var vi=GetLocalVariableInfo(reference, false);
      if(vi!=null) {
        var name=vi.Name;
        liveLocalVariableNames[name]=name;
      }
    }

    public void UndoNoteRead(IReference[] references) {
      foreach(var reference in references) {
        liveLocalVariableNames.Remove(reference.GetName());
      }
    }

    public bool CanProveIsNeverRead(IReference reference) {
      if(CodeGenerator.Instance.GeneratorHints.IsFirstPass
        || CodeGenerator.Instance.GetStaticVariableInfo(reference, false)!=null) {
        return false;
      }
      var vi=GetLocalVariableInfo(reference, true);
      return !BuilderHints.IsLiveLocalVariableName(vi.Name);
    }

    public object StartInflightUtterance(string message) {
      var cookie=new object();
      inflightUtterances.Add(cookie, message);
      return cookie;
    }

    public void FinishInflightUtterance(object cookie) {
      inflightUtterances.Remove(cookie);
    }
  }
}
