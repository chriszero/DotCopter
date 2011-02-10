using System.Collections;
using FluentInterop.CodeGeneration.Entities;
using FluentInterop.Coding;
using FluentInterop.Representations;
using Microsoft.SPOT;
using Math = System.Math;

namespace FluentInterop.CodeGeneration.RegisterAllocation {
  public sealed class VariableAllocatorHelper {
    private readonly int initialRegisterMask;
    private readonly ReadOnlyDictionary nsToLiveVariables;
    private readonly ReadOnlyDictionary nsToNumRegisters;

    /// <summary>
    /// Hashtable&lt;TerminalName,Representation&gt;
    /// </summary>
    private readonly Hashtable variableNameToRepresentation=new Hashtable();
    /// <summary>
    /// Hashtable&lt;TerminalName,AllocatorStats&gt;
    /// </summary>
    private readonly Hashtable memoizedInfo=new Hashtable();
    private bool runAgain=false;

    public VariableAllocatorHelper(int initialRegisterMask, ReadOnlyDictionary nsToLiveVariables, ReadOnlyDictionary nsToNumRegisters) {
      this.initialRegisterMask=initialRegisterMask;
      this.nsToLiveVariables=nsToLiveVariables;
      this.nsToNumRegisters=nsToNumRegisters;
    }

    public VariableAllocatorResult Doit(IEnumerable liveNonstaticVariableNames, ReadOnlyDictionary priorRepresentations) {
      foreach(TerminalName variableName in liveNonstaticVariableNames) {
        Recurse(variableName.Namespace);
      }
      var priorHt=priorRepresentations.ToHashtable();
      foreach(DictionaryEntry kvp in variableNameToRepresentation) {
        var variableName=(TerminalName)kvp.Key;
        var newRepresentation=(Representation)kvp.Value;
        var oldRepresentation=(Representation)priorHt[variableName];

        if(oldRepresentation==null) {
          Debug.Print("*** "+variableName+": allocated to "+newRepresentation);
          runAgain=true;
        } else if(!oldRepresentation.EqualTo(newRepresentation)) {
          Debug.Print("*** ".MyConcat(variableName, ": moved from ", oldRepresentation, " to ", newRepresentation));
          runAgain=true;
        }
        priorHt.Remove(variableName);
      }
      foreach(TerminalName variableName in priorHt.Keys) {
        Debug.Print("*** "+variableName+": removed");
      }

      var registersUsed=0;
      var allocatedVariableOffset=0;
      foreach(AllocatorStats stats in memoizedInfo.Values) {
        var usedRegisters=initialRegisterMask^stats.AllFreeRegisters;
        registersUsed|=usedRegisters;
        allocatedVariableOffset=Math.Min(allocatedVariableOffset, stats.LastUsedStackIndex);
      }
      return new VariableAllocatorResult(registersUsed, allocatedVariableOffset,
        variableNameToRepresentation.AsReadOnlyDictionary(), runAgain);
    }

    private AllocatorStats Recurse(Namespace ns) {
      var result=(AllocatorStats)memoizedInfo[ns];
      if(result!=null) {
        return result;
      }
      var innerStats=ns.Inner==null ? new AllocatorStats(initialRegisterMask, 0) : Recurse(ns.Inner);
      var variableNames=(ArrayList)nsToLiveVariables[ns];

      if(variableNames==null) {
        result=innerStats;
      } else {
        var rast=new RegisterAndStackAllocator(innerStats, variableNames.Count, (int)nsToNumRegisters[ns]);
        foreach(TerminalName variableName in variableNames) {
          var newRepresentation=rast.AllocateRepresentation(variableName);
          variableNameToRepresentation.Add(variableName, newRepresentation);
        }
        result=rast.MakeNewStats();
      }
      memoizedInfo.Add(ns, result);
      return result;
    }
  }
}
