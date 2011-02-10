using FluentInterop.CodeGeneration;
using FluentInterop.CodeGeneration.Entities;
using FluentInterop.OpCodes;

namespace FluentInterop.Representations {
  public sealed class Static : Representation {
    /// <summary>
    /// Canonical storage for a common case
    /// </summary>
    private static readonly int[] OneZero=new []{0};

    public readonly int DeclarationIndex;
    public readonly Label StorageLabel;
    public readonly int[] InitialData;

    public Static(int declarationIndex, Label storageLabel, int[] initialData) {
      this.DeclarationIndex=declarationIndex;
      if(initialData.Length==1 && initialData[0]==0) {
        initialData=OneZero;
      }
      this.StorageLabel=storageLabel;
      this.InitialData=initialData;
    }

    public override LowRegister ToRegister(LowRegister storageReg) {
      var emitter=CodeGenerator.Emitter;

      var address=StorageLabel.GetLabelAddressBestEffort();
      emitter.Emit(Format6OpCode.LDR, storageReg, address);
      return storageReg;
    }

    public override LowRegister ProposeRegisterOrScratch0() {
      return FuncBuilder.Instance.Scratch0;
    }

    public override void FromRegister(Register register) {
      var f=FuncBuilder.Instance;
      var emitter=CodeGenerator.Emitter;

      var scratch0=f.Scratch0;
      var scratch1=f.Scratch1;

      var lowRegister=register as LowRegister;
      if(lowRegister==null) {
        lowRegister=scratch0;
        emitter.EmitRegisterMoveIfDifferent(lowRegister, register);
      }
      var scratchToUse=lowRegister.Index==scratch0.Index ? scratch1 : scratch0;

      var address=StorageLabel.GetLabelAddressBestEffort();
      emitter.EmitLoadAddress(Format12OpCode.LDADDR_PC, scratchToUse, address, false);
      emitter.Emit(Format9OpCode.STR, lowRegister, scratchToUse, 0);
    }

    public override bool EqualTo(Representation obj) {
      var other=obj as Static;
      return other!=null && this.StorageLabel.Name.Equals(other.StorageLabel.Name);
    }
  }
}
