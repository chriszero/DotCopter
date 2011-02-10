using FluentInterop.BoolExpressions;
using FluentInterop.CodeGeneration;
using FluentInterop.Fluent;

namespace FluentInterop.CodeGeneration {
  partial class FuncBuilder {
    public While While(BoolExpression condition) {
      return new While(condition);
    }
  }
}

namespace FluentInterop.Fluent {
  public sealed class While {
    private readonly BoolExpression condition;
    private readonly object cookie;

    public While(BoolExpression condition) {
      this.condition=condition;
      this.cookie=FuncBuilder.Instance.StartInflightUtterance("While requires a Do");
    }

    public void Do(Action body) {
      var f=FuncBuilder.Instance;
      f.FinishInflightUtterance(cookie);
      using(f.OpenScope("while")) {
        var bodyLabel=f.DeclareLabel("body");
        var conditionLabel=f.DeclareLabel("condition");
        BranchLogic.UnconditionalBranchTo(conditionLabel);
        bodyLabel.Mark();
        body();
        conditionLabel.Mark();
        condition.BranchIf(true, bodyLabel);
      }
    }
  }
}
