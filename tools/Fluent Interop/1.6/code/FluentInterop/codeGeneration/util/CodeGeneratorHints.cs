using System.Collections;
using FluentInterop.Coding;

namespace FluentInterop.CodeGeneration.Util {
  public sealed class CodeGeneratorHints {
    public bool IsFirstPass { get; private set; }
    /// <summary>
    /// Hashtable&lt;TerminalName,int&gt;
    /// </summary>
    public ReadOnlyDictionary LabelNameToAddress { get; private set; }
    /// <summary>
    /// Hashtable&lt;string,FuncBuilderHints&gt;
    /// </summary>
    public ReadOnlyDictionary FuncNameToFuncBuilderHints { get; private set; }

    public CodeGeneratorHints() {
      IsFirstPass=true;
      var empty=new Hashtable().AsReadOnlyDictionary();
      LabelNameToAddress=empty;
      FuncNameToFuncBuilderHints=empty;
    }

    public CodeGeneratorHints CloneSetValues(ReadOnlyDictionary newLabelNameToAddress, ReadOnlyDictionary newFuncNameToFuncBuilderHints) {
      var result=(CodeGeneratorHints)this.MemberwiseClone();
      result.IsFirstPass=false;
      result.LabelNameToAddress=newLabelNameToAddress;
      result.FuncNameToFuncBuilderHints=newFuncNameToFuncBuilderHints;
      return result;
    }
  }
}
