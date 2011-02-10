using FluentInterop.CodeGeneration;
using FluentInterop.OpCodes;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public sealed class IndirectIntReference : IndirectReference {
    public IndirectIntReference(IntPointer baseAddress, IntExpression offset) : base(baseAddress, offset.ShiftLeft(2), 31*4+1) { }

    protected override IReadable EvaluateToHelper(IReference storage) {
      return base.EvaluateToHelper(storage,
        (srcReg, baseReg, offsetReg) => CodeGenerator.Emitter.Emit(Format7OpCode.LDR, srcReg, baseReg, offsetReg),
        (srcReg, baseReg, offsetValue) => CodeGenerator.Emitter.Emit(Format9OpCode.LDR, srcReg, baseReg, (byte)(offsetValue>>2)));
    }

    public IntExpression Value {
      set {
        base.AssignFromHelper(value,
          (srcReg, baseReg, offsetReg) => CodeGenerator.Emitter.Emit(Format7OpCode.STR, srcReg, baseReg, offsetReg),
          (srcReg, baseReg, offsetValue) =>
            CodeGenerator.Emitter.Emit(Format9OpCode.STR, srcReg, baseReg, (byte)(offsetValue>>2)));
      }
    }
  }
}
