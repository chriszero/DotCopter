using FluentInterop.CodeGeneration;
using FluentInterop.OpCodes;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public sealed class IntConstant : IntExpression, IReadable {
    private readonly int value;

    public IntConstant(int value) {
      this.value=value;
    }

    public override bool TryGetConstant(out int result) {
      result=value;
      return true;
    }

    protected override IReadable EvaluateToHelper(IReference storage) {
      return this;
    }

    public override bool IsNegated {
      get { return value<0; }
    }

    public RegisterOrByte ToRegisterOrUnsignedConstantHelper(LowRegister writeable, int exclusiveUpperBound) {
      return value>=0 && value<exclusiveUpperBound
        ? RegisterOrByte.Create((byte)value)
        : RegisterOrByte.Create(ToRegisterHelper(writeable));
    }

    public LowRegister ToRegisterHelper(LowRegister writeableReg) {
      var emitter=CodeGenerator.Emitter;
      if(value>=0 && value<256) {
        emitter.Emit(Format3OpCode.MOV, writeableReg, (byte)value);
      } else if(value<0 && value>-256) {
        emitter.Emit(Format3OpCode.MOV, writeableReg, 0);
        emitter.Emit(Format3OpCode.SUB, writeableReg, (byte)(-value));
      } else {
        var label=CodeGenerator.Instance.AllocateInlineConstant(value);
        var address=label.GetLabelAddressBestEffort();
        emitter.Emit(Format6OpCode.LDR, writeableReg, address);
      }
      return writeableReg;
    }
  }
}
