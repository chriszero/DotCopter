using FluentInterop.CodeGeneration;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public static class Assignment {
    public static void AssignAny(IReference lhs, Expression rhs) {
      var f=FuncBuilder.Instance;
      using(f.OpenScope("assign")) {
        if(f.CanProveIsNeverRead(lhs)) {
          rhs.EvaluateForItsSideEffects();
        } else {
          SpecialAssignAny(lhs, rhs);
        }
      }
    }

    public static void SpecialAssignAny(IReference lhs, Expression rhs) {
      var f=FuncBuilder.Instance;
      var lhsWasLivePriorToThisAssignment=f.IsLive(lhs);
      var rhsReadable=rhs.EvaluateTo(lhs);
      if(!ReferenceEquals(lhs, rhsReadable)) {
        var lhsReg=lhs.ProposeRegisterOrScratch0();
        var rhsReg=rhsReadable.ToRegister(lhsReg);
        lhs.FromRegister(rhsReg);
      }
      //Don't decide that a variable is live just because it was referenced (or it was used as temporary storage)
      //in its very-own assignment.
      if(!lhsWasLivePriorToThisAssignment) {
        f.UndoNoteRead(new[] {lhs});
      }
    }
  }
}