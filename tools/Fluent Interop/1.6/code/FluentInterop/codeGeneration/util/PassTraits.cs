using FluentInterop.CodeGeneration.RegisterAllocation;
using FluentInterop.Representations;

namespace FluentInterop.CodeGeneration.Util {
  public sealed class PassTraits {
    public readonly bool PreserveLinkRegister;
    public readonly LowRegister Scratch0;
    public readonly LowRegister Scratch1;
    public readonly bool Scratch0Used;
    public readonly bool Scratch1Used;
    public readonly int RegisterMask;
    public readonly int AllocatedVariableOffset;

    public PassTraits(bool preserveLinkRegister, LowRegister scratch0, LowRegister scratch1, bool scratch0Used, bool scratch1Used,
      int registerMaskExcludingScratch, int allocatedVariableOffset) {
      PreserveLinkRegister=preserveLinkRegister;
      Scratch0=scratch0;
      Scratch1=scratch1;
      Scratch0Used=scratch0Used;
      Scratch1Used=scratch1Used;
      RegisterMask=registerMaskExcludingScratch;
      if(Scratch0Used) {
        RegisterMask|=1<<(Scratch0.Index);
      }
      if(Scratch1Used) {
        RegisterMask|=1<<(Scratch1.Index);
      }
      AllocatedVariableOffset=allocatedVariableOffset;
    }

    public bool EqualTo(PassTraits other) {
      return this.PreserveLinkRegister==other.PreserveLinkRegister
        && this.Scratch0.EqualTo(other.Scratch0)
        && this.Scratch1.EqualTo(other.Scratch1)
        && this.Scratch0Used==other.Scratch0Used
        && this.Scratch1Used==other.Scratch1Used
        && this.RegisterMask==other.RegisterMask
        && this.AllocatedVariableOffset==other.AllocatedVariableOffset;
    }
  }
}
