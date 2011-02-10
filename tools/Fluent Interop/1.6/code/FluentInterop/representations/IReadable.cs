using FluentInterop.Expressions;

namespace FluentInterop.Representations {
  public interface IReadable {
  }

  public static class IReadable_Extensions {
    public static LowRegister ToRegister(this IReadable self, LowRegister writeableReg) {
      //special case for IntConstant
      var ic=self as IntConstant;
      return !ReferenceEquals(ic, null)
        ? ic.ToRegisterHelper(writeableReg) 
        : ((IReference)self).GetRepresentation().ToRegister(writeableReg);
    }

    public static RegisterOrByte ToRegisterOrByte(this IReadable self, LowRegister writeableReg) {
      return self.ToRegisterOrUnsignedConstant(256, writeableReg);
    }

    public static RegisterOrByte ToRegisterOrUint5(this IReadable self, LowRegister writeableReg) {
      return self.ToRegisterOrUnsignedConstant(32, writeableReg);
    }

    public static RegisterOrByte ToRegisterOrUnsignedConstant(this IReadable self, int exclusiveUpperBound, LowRegister writeableReg) {
      //special case for IntConstant
      var ic=self as IntConstant;
      return !ReferenceEquals(ic, null)
        ? ic.ToRegisterOrUnsignedConstantHelper(writeableReg, exclusiveUpperBound)
        : RegisterOrByte.Create(((IReference)self).GetRepresentation().ToRegister(writeableReg));
    }
  }
}
