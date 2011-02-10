using FluentInterop.CodeGeneration;
using FluentInterop.CodeGeneration.Entities;

namespace FluentInterop.BoolExpressions {
  public class And : BinaryBoolExpression {
    public And(BoolExpression lhs, BoolExpression rhs) : base(lhs, rhs) {}

    public override void BranchIf(bool sense, Label target) {
      var f=FuncBuilder.Instance;
      if(!sense) {
        //if you want lhs&&rhs to be false, then you will succeed if lhs is false or rhs is false
        lhs.BranchIf(false, target);
        rhs.BranchIf(false, target);
      } else {
        //but if you want lhs&&rhs to be true, then you will succeed if lhs is true and rhs is true
        var fail=f.DeclareLabel("fail");
        lhs.BranchIf(false, fail);
        rhs.BranchIf(true, target);
        fail.Mark();
      }
    }

    protected override BoolExpression Invert() {
      return new Or(!lhs, !rhs);
    }
  }
}
