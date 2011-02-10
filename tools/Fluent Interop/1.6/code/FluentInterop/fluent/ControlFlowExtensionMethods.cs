using FluentInterop.CodeGeneration;
using FluentInterop.Expressions;
using FluentInterop.OpCodes;
using FluentInterop.Representations;

namespace FluentInterop.Fluent {
  public static class ControlFlowExtensionMethods {
    public static void Return(this FuncBuilder f, IntExpression expr) {
      using(f.OpenScope("return")) {
        var emitter=CodeGenerator.Emitter;
        var resultStorage=f.Declare.Int("result");
        var exprResult=expr.EvaluateTo(resultStorage);

        var exprRegOrByte=exprResult.ToRegisterOrByte(f.Scratch0);
        if(exprRegOrByte.IsRegister) {
          emitter.EmitRegisterMoveIfDifferent(Register.R0, exprRegOrByte.Register);
        } else {
          emitter.Emit(Format3OpCode.MOV, Register.R0, exprRegOrByte.Byte);
        }
        BranchLogic.UnconditionalBranchTo(f.TheExitLabel);
      }
    }
  }
}
