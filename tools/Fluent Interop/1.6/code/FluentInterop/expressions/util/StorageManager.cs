using System;
using FluentInterop.CodeGeneration;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public class StorageManager {
    private readonly IReference storage;
    private readonly IReference temp;

    public StorageManager(IReference storage) {
      var g=CodeGenerator.Instance;
      var f=FuncBuilder.Instance;
      //don't use statics for storage
      this.storage=g.GetStaticVariableInfo(storage, false)!=null ? f.Declare.Int("temp2") : storage;
      this.temp=f.Declare.Int("temp");
    }

    public IReference ForLhs(Expression rhs) {
      return rhs.HasConflictWith(storage) ? temp : storage;
    }

    public IReference ForRhs(IReadable lhsResult) {
      var result=ReferenceEquals(lhsResult, storage) ? temp : storage;
      if(result==null) {
        throw new Exception("sad");
      }
      return result;
    }
  }
}