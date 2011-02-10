using FluentInterop.CodeGeneration;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public static class IntExpressionHelper {
    public static IReadable Invoke(string scopeName, IReference storage, IntExpression lhs, IntExpression rhs,
      ActionOnThreeLowRegisters registerAction, ActionOnTwoLowRegistersAndAByte byteAction) {
      var f=FuncBuilder.Instance;

      using(f.OpenScope(scopeName)) {
        var sm=new StorageManager(storage);
        var lhsResult=lhs.EvaluateTo(sm.ForLhs(rhs));
        var rhsResult=rhs.EvaluateTo(sm.ForRhs(lhsResult));

        var storageReg=storage.ProposeRegisterOrScratch0();
        var lhsReg=lhsResult.ToRegister(f.Scratch0);
        var rhsRegOrByte=rhsResult.ToRegisterOrByte(f.Scratch1);
        if(rhsRegOrByte.IsRegister) {
          registerAction(storageReg, lhsReg, rhsRegOrByte.Register);
        } else {
          byteAction(storageReg, lhsReg, rhsRegOrByte.Byte);
        }
        storage.FromRegister(storageReg);
        return storage;
      }
    }
  }
}