using FluentInterop.CodeGeneration;
using FluentInterop.OpCodes;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public sealed class Add : BinaryIntExpression {
    public static IntExpression Create(IntExpression lhs, IntExpression rhs) {
      //item 1: move constants to the right hand side
      //item 2: move negated items to the right hand side, except where this conflicts with item 1
      if(lhs.IsConstant() || (lhs.IsNegated && !rhs.IsConstant())) {
        return CreateHelper(rhs, lhs);
      }
      return CreateHelper(lhs, rhs);
    }

    private static Add CreateHelper(IntExpression lhs, IntExpression rhs) {
      //turn a+(-b) into a-(+b)
      return rhs.IsNegated
        ? new Add(lhs, -rhs, false)
        : new Add(lhs, rhs, true);
    }

    private readonly bool isAdd;

    private Add(IntExpression lhs, IntExpression rhs, bool isAdd) : base(lhs, rhs) {
      this.isAdd=isAdd;
    }

    protected override IReadable EvaluateToHelper(IReference storage) {
      Format2OpCode registerOr3BitImmediateOpCode;
      Format3OpCode immediate8OpCode;
      if(isAdd) {
        registerOr3BitImmediateOpCode=Format2OpCode.ADD;
        immediate8OpCode=Format3OpCode.ADD;
      } else {
        registerOr3BitImmediateOpCode=Format2OpCode.SUB;
        immediate8OpCode=Format3OpCode.SUB;
      }
      var opCodeString=registerOr3BitImmediateOpCode.ToHumanReadable();
      return IntExpressionHelper.Invoke(opCodeString, storage, lhs, rhs,
        (resultReg, lhsReg, rhsReg) => CodeGenerator.Emitter.Emit(registerOr3BitImmediateOpCode, resultReg, lhsReg, rhsReg),
        (resultReg, lhsReg, value) => {
          var emitter=CodeGenerator.Emitter;
          if(value<8) {
            emitter.Emit(registerOr3BitImmediateOpCode, resultReg, lhsReg, value);
          } else {
            emitter.EmitRegisterMoveIfDifferent(resultReg, lhsReg);
            emitter.Emit(immediate8OpCode, resultReg, value);
          }
        });
    }
  }
}
