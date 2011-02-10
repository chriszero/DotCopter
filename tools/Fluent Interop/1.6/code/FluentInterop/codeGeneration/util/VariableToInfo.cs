using System;
using System.Collections;
using FluentInterop.Coding;
using FluentInterop.Representations;

namespace FluentInterop.CodeGeneration.Util {
  public sealed class VariableToInfo {
    private readonly VariableToInfo inner;
    /// <summary>
    /// Hashtable&lt;IReference,VariableInfo&gt;
    /// </summary>
    private readonly Hashtable variables=new Hashtable();

    public VariableToInfo(VariableToInfo inner=null) {
      this.inner=inner;
    }

    public VariableToInfo Nest() {
      return new VariableToInfo(this);
    }

    public VariableToInfo UnNest() {
      return this.inner;
    }

    public void Add(IReference variable, LocalVariableInfo localVariableInfo) {
      variables.CheckedAdd(variable, localVariableInfo);
    }

    public void MigrateR0R3ToStack(out int registerMaskToSpill, out int stackConsumption) {
      var outermost=this.variables;
      var variableNamesToMigrate=new ArrayList[4];
      for(var i=0; i<4; ++i) {
        variableNamesToMigrate[i]=new ArrayList();
      }
      for(var self=this.inner; self!=null; self=self.inner) {
        foreach(DictionaryEntry kvp in self.variables) {
          var vi=(LocalVariableInfo)kvp.Value;
          var register=vi.Representation as LowRegister;
          if(register!=null) {
            var index=register.Index;
            if(index<4) {
              variableNamesToMigrate[index].Add(kvp);
            }
          }
        }
      }
      registerMaskToSpill=0;
      stackConsumption=0;
      var f=FuncBuilder.Instance;
      for(var i=3; i>=0; --i) {
        foreach(DictionaryEntry de in variableNamesToMigrate[i]) {
          var variable=(IReference)de.Key;
          var vi=(LocalVariableInfo)de.Value;
          stackConsumption-=4;
          var newVariableInfo=new LocalVariableInfo(vi.Name, new StackWordRelativeToZero(f.StackPointer+stackConsumption));
          outermost.CheckedAdd(variable, newVariableInfo);
          registerMaskToSpill|=(1<<i);
        }
      }
    }

    public LocalVariableInfo LookupExists(IReference variable, bool strict) {
      for(var self=this; self!=null; self=self.inner) {
        var result=(LocalVariableInfo)self.variables[variable];
        if(result!=null) {
          return result;
        }
      }
      if(strict) {
        throw new Exception("not found");
      }
      return null;
    }
  }
}
