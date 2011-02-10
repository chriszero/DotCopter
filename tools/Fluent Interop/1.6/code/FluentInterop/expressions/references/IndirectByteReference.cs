using FluentInterop.CodeGeneration;
using FluentInterop.OpCodes;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public class IndirectByteReference : IndirectReference {
    public IndirectByteReference(BytePointer baseAddress, IntExpression offset) : base(baseAddress, offset, 32) {}

    protected override IReadable EvaluateToHelper(IReference storage) {
      return base.EvaluateToHelper(storage,
        (destReg, baseReg, offsetReg) => CodeGenerator.Emitter.Emit(Format7OpCode.LDRB, destReg, baseReg, offsetReg),
        (destReg, baseReg, offsetValue) => CodeGenerator.Emitter.Emit(Format9OpCode.LDRB, destReg, baseReg, offsetValue));
    }

    public IntExpression Value {
      set {
        base.AssignFromHelper(value,
          (srcReg, baseReg, offsetReg) => CodeGenerator.Emitter.Emit(Format7OpCode.STRB, srcReg, baseReg, offsetReg),
          (srcReg, baseReg, offsetValue) => CodeGenerator.Emitter.Emit(Format9OpCode.STRB, srcReg, baseReg, offsetValue));
      }
    }
  }
}
