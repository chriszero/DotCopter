using FluentInterop.CodeGeneration;
using FluentInterop.CodeGeneration.Entities;

namespace FluentInterop.BoolExpressions {
  public class Or : BinaryBoolExpression {
    public Or(BoolExpression lhs, BoolExpression rhs) : base(lhs, rhs) {}

    public override void BranchIf(bool sense, Label target) {
      if(!sense) {
        //if you want lhs||rhs to be false, then you will succeed if lhs is false and rhs is false
        Invert().BranchIf(true, target);
      } else {
        //if you want lhs||rhs to be true, then you will succeed if lhs is true or rhs is true
        lhs.BranchIf(true, target);
        rhs.BranchIf(true, target);
      }
    }

    protected override BoolExpression Invert() {
      return new And(!lhs, !rhs);
    }
  }
}
