using System;
using System.Collections;
using FluentInterop.CodeGeneration.Entities;
using FluentInterop.Coding;
using Math = System.Math;

namespace FluentInterop.CodeGeneration.RegisterAllocation {
  public static class VariableAllocator {
    /// <summary>
    /// The leaves start out with the maximum register budget
    /// They take what they want and hand the remainder to their parent
    /// This proceeds all the way to the root, which gets whatever is left over
    /// </summary>
    public static VariableAllocatorResult Run(IEnumerable liveLocalNonParameterVariableNames,
      int freeRegisterMask,
      ReadOnlyDictionary priorRepresentations) {

      var nsToLiveVars=CreateNamespaceToVariablesAllocatedTherein(liveLocalNonParameterVariableNames);

      int maxDepth;
      var depthToNamespaces=MakeDepthToNamespaces(liveLocalNonParameterVariableNames, out maxDepth);
      var leaves=FindLeaves(liveLocalNonParameterVariableNames);

      var nsToNumRegisters=EstablishRegisterAllocationCounts(depthToNamespaces, freeRegisterMask, leaves.Keys, maxDepth, nsToLiveVars);
      var va=new VariableAllocatorHelper(freeRegisterMask, nsToLiveVars, nsToNumRegisters);
      return va.Doit(liveLocalNonParameterVariableNames, priorRepresentations);
    }

    /// <returns>Hashtable&lt;Namespace,ArrayList&lt;TerminalName&gt;&gt;</returns>
    private static ReadOnlyDictionary CreateNamespaceToVariablesAllocatedTherein(IEnumerable liveLocalNonParameterVariableNames) {
      var result=new Hashtable();
      foreach(TerminalName name in liveLocalNonParameterVariableNames) {
        result.LookupOrCreateArrayList(name.Namespace).Add(name);
      }
      return result.AsReadOnlyDictionary();
    }

    /// <summary>
    /// Basically: liveVariables.Select(lv=>lv.Namespace).GroupBy(ns=>ns.Depth)
    /// </summary>
    /// <returns>Hashtable&lt;int,ArrayList&lt;Namespace&gt;&gt;</returns>
    private static ReadOnlyDictionary MakeDepthToNamespaces(IEnumerable liveLocalNonParameterVariableNames, out int maxDepth) {
      var result=new Hashtable();
      var beenHere=new Hashtable();
      maxDepth=-1;
      foreach(TerminalName name in liveLocalNonParameterVariableNames) {
        var ns=name.Namespace;
        maxDepth=Math.Max(maxDepth, ns.Depth);
        while(ns!=null) {
          if(beenHere.Contains(ns)) {
            break;
          }
          beenHere.Add(ns, null);
          result.LookupOrCreateArrayList(ns.Depth).Add(ns);
          ns=ns.Inner;
        }
      }
      return result.AsReadOnlyDictionary();
    }

    /// <summary>
    /// Find the namespace leaves
    /// </summary>
    /// <returns>Hashtable&lt;Namespace,null&gt;&gt;</returns>
    private static ReadOnlyDictionary FindLeaves(IEnumerable liveLocalNonParameterVariableNames) {
      var leaves=new Hashtable(); //Hashtable<Namespace,null>
      foreach(TerminalName name in liveLocalNonParameterVariableNames) {
        leaves[name.Namespace]=null;
      }

      foreach(TerminalName name in liveLocalNonParameterVariableNames) {
        for(var ns=name.Namespace.Inner; ns!=null; ns=ns.Inner) {
          leaves.Remove(ns);
        }
      }
      return leaves.AsReadOnlyDictionary();
    }

    private static ReadOnlyDictionary EstablishRegisterAllocationCounts(ReadOnlyDictionary depthToNamespaces, int freeRegisterMask,
      IEnumerable leaves, int maxDepth, ReadOnlyDictionary nsToLiveVars) {

      var initialBudget=0;
      while(freeRegisterMask>0) {
        ++initialBudget;
        freeRegisterMask&=(freeRegisterMask-1);
      }

      var budget=new Hashtable(); //Hashtable<Namespace,int>
      foreach(Namespace ns in leaves) {
        budget[ns]=initialBudget;
      }

      var result=new Hashtable(); //Hashtable<Namespace,int>
      object boxedMaxValue=int.MaxValue;
      for(var depth=maxDepth; depth>=0; --depth) {
        var itemsToProcess=(ArrayList)depthToNamespaces[depth];
        foreach(Namespace ns in itemsToProcess) {
          var capacity=(int)budget[ns];
          var liveVars=(ArrayList)nsToLiveVars[ns];
          var requested=liveVars!=null ? liveVars.Count : 0;
          var granted=Math.Min(capacity, requested);
          var remaining=capacity-granted;
          result.Add(ns, granted);
          result[ns]=granted;

          //now forward the budget to my parent
          var inner=ns.Inner;
          if(inner!=null) {
            var existingBudget=(int)(budget[inner] ?? boxedMaxValue);
            var newBudget=Math.Min(existingBudget, remaining);
            budget[inner]=newBudget;
          }
        }
      }
      return result.AsReadOnlyDictionary();
    }
  }
}
