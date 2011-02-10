using FluentInterop.CodeGeneration;
using FluentInterop.OpCodes;

namespace FluentInterop.Representations {
  /// <summary>
  /// Stack layout.  where BASE is the stack pointer at method entry
  /// BASE+12: param3
  /// BASE+ 8: param2
  /// BASE+ 4: param1
  /// BASE+ 0: param0
  /// BASE- 4: LR (maybe)
  /// BASE- 8: REG1 (maybe)
  /// BASE-12: REG0 (maybe)  &lt;== end of variables
  /// BASE-16: var 2
  /// BASE-20: var 1
  /// BASE-24: var 0  &lt;== start of variables
  /// BASE-28: junk
  /// BASE-32: junk
  /// BASE-36: junk
  /// BASE-40: junk
  /// BASE-44         &lt;== SP
  /// 
  /// Q: what is the offset from the current SP?
  ///  endOfVars=BASE-12
  ///  var2=endOfVars-4
  ///  SP=BASE-44
  ///  var2-SP=endOfVars-4-(BASE-44)
  ///         =(BASE-12)-4-(BASE-44)
  ///         =-16+44
  /// 
  /// therefore endOfVars.offsetFromBase+variable.offsetFromEndOfVars-stackPointer.offsetFromBase
  /// </summary>
  public abstract class StackWord : Representation {
    public override LowRegister ToRegister(LowRegister proposal) {
      CodeGenerator.Emitter.Emit(Format11OpCode.LDR, proposal, (uint)CalculateOffset());
      return proposal;
    }

    public override LowRegister ProposeRegisterOrScratch0() {
      return FuncBuilder.Instance.Scratch0;
    }

    public override void FromRegister(Register register) {
      var f=FuncBuilder.Instance;
      var emitter=CodeGenerator.Emitter;

      var lowRegister=register as LowRegister;
      if(lowRegister==null) {
        lowRegister=f.Scratch0;
        emitter.EmitRegisterMoveIfDifferent(lowRegister, register);
      }
      emitter.Emit(Format11OpCode.STR, lowRegister, (uint)CalculateOffset());
    }

    protected abstract int CalculateOffset();
  }
}
