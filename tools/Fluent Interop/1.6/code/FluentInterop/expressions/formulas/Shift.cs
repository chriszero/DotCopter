using FluentInterop.CodeGeneration;
using FluentInterop.Representations;
using FluentInterop.OpCodes;

namespace FluentInterop.Expressions {
  public sealed class Shift : BinaryIntExpression {
    public static IntExpression Create(IntExpression lhs, IntExpression rhs, bool shiftLeft) {
      int lhsValue, rhsValue;
      if(lhs.TryGetConstant(out lhsValue) && rhs.TryGetConstant(out rhsValue)) {
        return shiftLeft ? lhsValue<<rhsValue : lhsValue>>rhsValue;
      }
      return new Shift(lhs, rhs, shiftLeft);
    }

    private readonly bool shiftLeft;

    private Shift(IntExpression lhs, IntExpression rhs, bool shiftLeft) : base(lhs, rhs) {
      this.shiftLeft=shiftLeft;
    }

    protected override IReadable EvaluateToHelper(IReference storage) {
      Format4OpCode registerOpCode;
      Format1OpCode immediateOpCode;
      if(shiftLeft) {
        registerOpCode=Format4OpCode.LSL;
        immediateOpCode=Format1OpCode.LSL;
      } else {
        registerOpCode=Format4OpCode.ASR;
        immediateOpCode=Format1OpCode.ASR;
      }

      var f=FuncBuilder.Instance;
      var emitter=CodeGenerator.Emitter;

      using(f.OpenScope(registerOpCode.ToHumanReadable())) {
        var sm=new StorageManager(storage);
        var lhsResult=lhs.EvaluateTo(sm.ForLhs(rhs));
        var rhsResult=rhs.EvaluateTo(sm.ForRhs(lhsResult));

        var scratch0=f.Scratch0;
        var scratch1=f.Scratch1;

        var storageReg=scratch0;
        var lhsReg=lhsResult.ToRegister(scratch0);
        var rhsRegOrOffset=rhsResult.ToRegisterOrUint5(scratch1);
        if(rhsRegOrOffset.IsRegister) {
          emitter.EmitRegisterMoveIfDifferent(storageReg, lhsReg);
          emitter.Emit(registerOpCode, storageReg, rhsRegOrOffset.Register);
        } else {
          emitter.Emit(immediateOpCode, storageReg, lhsReg, rhsRegOrOffset.Byte);
        }
        storage.FromRegister(storageReg);
        return storage;
      }
    }
  }
}
