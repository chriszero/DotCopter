using FluentInterop.CodeGeneration.Entities;
using FluentInterop.OpCodes;
using Microsoft.SPOT;

namespace FluentInterop.CodeGeneration {
  public static class BranchLogic {
    public static void UnconditionalBranchTo(Label target) {
      var emitter=CodeGenerator.Emitter;
      var targetAddress=target.GetLabelAddressBestEffort();
      var caddr=emitter.CurrentAddress;
      //branch to next instruction or branch to self cause this instruction to be elided
      //why: in phase N, a needless branch will look like a branch to the next instruction,
      // but in phase N+1 it will look like a branch to self
      if(targetAddress==caddr || targetAddress==caddr+2) {
        Debug.Print("<branch to next instruction removed>");
      } else {
        emitter.Emit(Format18OpCode.B, targetAddress);
      }
    }
  }
}
