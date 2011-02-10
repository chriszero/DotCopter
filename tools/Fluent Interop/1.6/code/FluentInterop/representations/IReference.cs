using FluentInterop.CodeGeneration;
using FluentInterop.CodeGeneration.Entities;

namespace FluentInterop.Representations {
  public interface IReference : IReadable {
  }

  public static class IReference_Extensions {
    public static LowRegister ProposeRegisterOrScratch0(this IReference self) {
      return self.GetRepresentation().ProposeRegisterOrScratch0();
    }

    public static void FromRegister(this IReference self, Register register) {
      self.GetRepresentation().FromRegister(register);
    }

    public static TerminalName GetName(this IReference self) {
      return FuncBuilder.Instance.GetStaticOrLocalVariableInfo(self, true).Name;
    }

    public static Representation GetRepresentation(this IReference self) {
      return FuncBuilder.Instance.GetStaticOrLocalVariableInfo(self, true).Representation;
    }
  }
}
