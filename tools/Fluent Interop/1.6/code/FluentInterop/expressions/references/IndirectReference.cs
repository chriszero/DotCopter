using FluentInterop.CodeGeneration;
using FluentInterop.OpCodes;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public abstract class IndirectReference : IntExpression {
    protected readonly Pointer baseAddress;
    protected readonly IntExpression offset;
    protected readonly int exclusiveUpperBoundForConstantOffset;

    protected IndirectReference(Pointer baseAddress, IntExpression offset, int exclusiveUpperBoundForConstantOffset) {
      this.baseAddress=baseAddress;
      this.offset=offset;
      this.exclusiveUpperBoundForConstantOffset=exclusiveUpperBoundForConstantOffset;
    }

    protected IReadable EvaluateToHelper(IReference storage, ActionOnThreeLowRegisters regAction, ActionOnTwoLowRegistersAndAByte byteAction) {
      var f=FuncBuilder.Instance;
      using(f.OpenScope("indirectReferenceEval")) {
        var declarer=f.Declare;
        var baseStorage=declarer.Int("base");
        var offsetStorage=declarer.Int("offset");

        var baseResult=baseAddress.EvaluateTo(baseStorage);
        var offsetResult=offset.EvaluateTo(offsetStorage);

        var storageReg=storage.ProposeRegisterOrScratch0();
        var baseReg=baseResult.ToRegister(f.Scratch0);
        var offsetRegOrByte=offsetResult.ToRegisterOrUnsignedConstant(exclusiveUpperBoundForConstantOffset, f.Scratch1);
        if(offsetRegOrByte.IsRegister) {
          regAction(storageReg, baseReg, offsetRegOrByte.Register);
        } else {
          byteAction(storageReg, baseReg, offsetRegOrByte.Byte);
        }
        storage.FromRegister(storageReg);
        return storage;
      }
    }

    public override bool HasConflictWith(IReference storage) {
      return baseAddress.HasConflictWith(storage) || offset.HasConflictWith(storage);
    }

    protected void AssignFromHelper(IntExpression value, ActionOnThreeLowRegisters regAction, ActionOnTwoLowRegistersAndAByte byteAction) {
      var f=FuncBuilder.Instance;

      using(f.OpenScope("indirectReferenceAssign")) {
        var declarer=f.Declare;
        var valueTemp=declarer.Int("value");
        var baseTemp=declarer.Int("baseAddress");
        var offsetTemp=declarer.Int("offset");

        var valueResult=value.EvaluateTo(valueTemp);
        var baseResult=baseAddress.EvaluateTo(baseTemp);
        var offsetResult=offset.EvaluateTo(offsetTemp);

        //Oh crap. I need three readable registers to accomplish this instruction.
        //The other instructions only need two registers.
        //If indeed I do need three scratch registers, I'm going to do the super hack job from hell
        //(namely, swapping some other register with LR)
        var baseReg=baseResult.ToRegister(f.Scratch0);
        var offsetRegOrByte=offsetResult.ToRegisterOrUnsignedConstant(exclusiveUpperBoundForConstantOffset, f.Scratch1);
        if(offsetRegOrByte.IsRegister) {
          var offsetReg=offsetRegOrByte.Register;
          var valuePropReg=ProposeRegisterForValue(valueResult, baseReg, offsetReg);
          LowRegister valueReg;
          if(valuePropReg!=null) {
            valueReg=valueResult.ToRegister(valuePropReg);
          } else {
            //out of scratch registers.  Burn another instruction to do base+=offset so that you can free up offset
            CodeGenerator.Emitter.Emit(Format2OpCode.ADD, baseReg, offsetReg, 0);
            //now the register in offsetReg, which is known to be scratch, can be used to hold the value
            valueReg=valueResult.ToRegister(offsetReg);
          }
          regAction(valueReg, baseReg, offsetReg);
        } else {
          var valueReg=valueResult.ToRegister(f.Scratch1);
          byteAction(valueReg, baseReg, offsetRegOrByte.Byte);
        }
      }
    }

    private static LowRegister ProposeRegisterForValue(IReadable valueResult, LowRegister baseReg, LowRegister offsetReg) {
      var f=FuncBuilder.Instance;
      var scratch0=f.Scratch0;

      if(baseReg.Index!=scratch0.Index) {
        return scratch0;
      }

      var scratch1=f.Scratch1;
      if(offsetReg.Index!=scratch1.Index) {
        return scratch1;
      }
      var reference=valueResult as IReference;
      if(reference!=null) {
        var lowRegister=reference.GetRepresentation() as LowRegister;
        if(lowRegister!=null) {
          return lowRegister;
        }
      }
      return null;
    }
  }
}
