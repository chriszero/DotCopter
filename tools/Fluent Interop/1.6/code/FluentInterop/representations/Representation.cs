using System;

namespace FluentInterop.Representations {
  public abstract class ReadableRepresentation {
    public abstract LowRegister ToRegister(LowRegister proposal);
  }

  public abstract class Representation : ReadableRepresentation {
    public abstract LowRegister ProposeRegisterOrScratch0();
    public abstract void FromRegister(Register register);

    public abstract bool EqualTo(Representation other);

    public sealed override bool Equals(object obj) {
      throw new NotImplementedException("NIY");
    }
    public sealed override int GetHashCode() {
      throw new NotImplementedException("NIY");
    }
  }
}
