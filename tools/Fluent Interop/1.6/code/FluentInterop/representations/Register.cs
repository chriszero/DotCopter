using System;
using FluentInterop.CodeGeneration;

namespace FluentInterop.Representations {
  public abstract class Register : Representation {
    public static LowRegister R0 { get { return new LowRegister(0); } }
    public static HighRegister LR { get { return new HighRegister(14); } }
    public static HighRegister PC { get { return new HighRegister(15); } }

    public readonly int Index;

    protected Register(int index) {
      Index=index;
    }

    public override bool EqualTo(Representation other) {
      var otherReg=other as Register;
      return otherReg!=null && this.Index==otherReg.Index;
    }

    public override string ToString() {
      return "R"+Index;
    }
  }

  public sealed class LowRegister : Register {
    public LowRegister(int index) : base(index) {
      if(index>=8) {
        throw new Exception("bad index");
      }
    }

    public override LowRegister ToRegister(LowRegister proposal) {
      return this;
    }

    public override LowRegister ProposeRegisterOrScratch0() {
      return this;
    }

    public override void FromRegister(Register register) {
      CodeGenerator.Emitter.EmitRegisterMoveIfDifferent(this, register);
    }
  }

  public class HighRegister : Register {
    public HighRegister(int index) : base(index) {
      if(index<8) {
        throw new Exception("bad index");
      }
    }

    public override LowRegister ToRegister(LowRegister proposal) {
      throw new NotImplementedException();
    }

    public override LowRegister ProposeRegisterOrScratch0() {
      throw new NotImplementedException();
    }

    public override void FromRegister(Register register) {
      throw new NotImplementedException();
    }
  }
}
