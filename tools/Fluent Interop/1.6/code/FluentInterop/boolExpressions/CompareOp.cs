using System;
using FluentInterop.CodeGeneration;
using FluentInterop.CodeGeneration.Entities;
using FluentInterop.Expressions;
using FluentInterop.OpCodes;
using FluentInterop.Representations;

namespace FluentInterop.BoolExpressions {
  public sealed class CompareOp : BoolExpression {
    public static CompareOp CreateEqual(IntExpression lhs, IntExpression rhs) {
      return CreateHelper(lhs, rhs, Format16OpCode.BEQ, Format16OpCode.BEQ, Format16OpCode.BNE, Format16OpCode.BNE);
    }
    public static CompareOp CreateNotEqual(IntExpression lhs, IntExpression rhs) {
      return CreateHelper(lhs, rhs, Format16OpCode.BNE, Format16OpCode.BNE, Format16OpCode.BEQ, Format16OpCode.BEQ);
    }
    public static CompareOp CreateLessThan(IntExpression lhs, IntExpression rhs) {
      return CreateHelper(lhs, rhs, Format16OpCode.BLT, Format16OpCode.BGT, Format16OpCode.BGE, Format16OpCode.BLE);
    }
    public static CompareOp CreateLessThanOrEqual(IntExpression lhs, IntExpression rhs) {
      return CreateHelper(lhs, rhs, Format16OpCode.BLE, Format16OpCode.BGE, Format16OpCode.BGT, Format16OpCode.BLT);
    }

    // branchOpCode: the normal case: (a<b) therefore BLT
    // flipOpCode: the flipped case: (a<b)==(b>a), therefore BGT
    // inverseOpCode: the inverse case: (a<b)==!(a>=b), therefore BGE
    // flipInverseOpCode: the flipped inverse case: (a<b)==!(b<=a), therefore BLE
    private static CompareOp CreateHelper(IntExpression lhs, IntExpression rhs,
      Format16OpCode branchOpCode, Format16OpCode flipOpCode, Format16OpCode inverseOpCode, Format16OpCode flipInverseOpCode) {
      if(lhs.IsConstant()) {
        if(rhs.IsConstant()) {
          throw new Exception("ridiculous");
        }
        //flip the constant over to the right side, for better code generation
        return new CompareOp(rhs, lhs, flipOpCode, flipInverseOpCode);
      }
      return new CompareOp(lhs, rhs, branchOpCode, inverseOpCode);
    }

    private readonly IntExpression lhs;
    private readonly IntExpression rhs;
    private readonly Format16OpCode branchOpCode; //for example, BLT
    private readonly Format16OpCode inverseOpCode; //should we wish to invert this operator, because (a<b)==!(a>=b), therefore BGE

    private CompareOp(IntExpression lhs, IntExpression rhs, Format16OpCode branchOpCode, Format16OpCode inverseOpCode) {
      this.lhs=lhs;
      this.rhs=rhs;
      this.branchOpCode=branchOpCode;
      this.inverseOpCode=inverseOpCode;
    }

    public override void BranchIf(bool sense, Label target) {
      var f=FuncBuilder.Instance;
      var emitter=CodeGenerator.Emitter;
      var declarer=f.Declare;
      using(f.OpenScope("compare")) {
        var lhsTemp=declarer.Int("lhs");
        var rhsTemp=declarer.Int("rhs");

        var lhsResult=lhs.EvaluateTo(lhsTemp);
        var rhsResult=rhs.EvaluateTo(rhsTemp);

        var lhsReg=lhsResult.ToRegister(f.Scratch0);
        var rhsRegOrByte=rhsResult.ToRegisterOrByte(f.Scratch1);
        if(rhsRegOrByte.IsRegister) {
          emitter.Emit(Format4OpCode.CMP, lhsReg, rhsRegOrByte.Register);
        } else {
          emitter.Emit(Format3OpCode.CMP, lhsReg, rhsRegOrByte.Byte);
        }

        var instructionToUse=sense ? branchOpCode : inverseOpCode;
        var address=target.GetLabelAddressBestEffort();
        emitter.Emit(instructionToUse, address);
      }
    }

    protected override BoolExpression Invert() {
      return new CompareOp(lhs, rhs, inverseOpCode, branchOpCode);
    }
  }
}
