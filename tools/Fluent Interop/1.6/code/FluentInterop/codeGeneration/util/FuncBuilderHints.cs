using System.Collections;
using FluentInterop.CodeGeneration.Entities;
using FluentInterop.Coding;
using FluentInterop.Representations;
using Microsoft.SPOT;

namespace FluentInterop.CodeGeneration.Util {
  public sealed class FuncBuilderHints {
    public PassTraits PassTraits { get; private set; }

    private ReadOnlyDictionary liveLocalVariableNames;

    /// <summary>
    /// KeyLookup&lt;TerminalName,Representation&gt;
    /// </summary>
    public ReadOnlyDictionary VariableNameToRepresentation { get; private set; }


    public FuncBuilderHints() {
      this.PassTraits=new PassTraits(true, new LowRegister(Traits.Scratch0Index), new LowRegister(Traits.Scratch1Index),
        true, true, 0x3f, -Traits.MaxStackSize);
      var empty=new Hashtable().AsReadOnlyDictionary();
      this.VariableNameToRepresentation=empty;
    }

    private FuncBuilderHints Clone() {
      return (FuncBuilderHints)this.MemberwiseClone();
    }

    public FuncBuilderHints CloneSetValues(ReadOnlyDictionary newLiveLocalVariableNames,
      ReadOnlyDictionary newVariableNameToRepresentation, PassTraits newPassTraits) {
      var result=Clone();
      result.liveLocalVariableNames=newLiveLocalVariableNames;
      result.VariableNameToRepresentation=newVariableNameToRepresentation;
      result.PassTraits=newPassTraits;
      return result;
    }

    public bool TryGetRepresentation(TerminalName refName, out Representation result) {
      result=(Representation)VariableNameToRepresentation[refName];
      return result!=null;
    }

    public bool IsLiveLocalVariableName(TerminalName name) {
      return liveLocalVariableNames.ContainsKey(name);
    }
  }
}
