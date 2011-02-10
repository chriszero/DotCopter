using FluentInterop.CodeGeneration;
using FluentInterop.Fluent;

namespace FluentInterop.CodeGeneration {
  partial class FuncBuilder {
    public For For(ActionOnIntVar initialize, IntExprToBoolExpr condition, ActionOnIntVar increment) {
      return new For(initialize, condition, increment);
    }
  }
}

namespace FluentInterop.Fluent {
  public class For {
    private readonly ActionOnIntVar initialize;
    private readonly IntExprToBoolExpr condition;
    private readonly ActionOnIntVar increment;
    private readonly object cookie;

    public For(ActionOnIntVar initialize, IntExprToBoolExpr condition, ActionOnIntVar increment) {
      this.initialize=initialize;
      this.condition=condition;
      this.increment=increment;
      this.cookie=FuncBuilder.Instance.StartInflightUtterance("For requires a Do");
    }

    public void Do(ActionOnIntVar body) {
      var f=FuncBuilder.Instance;
      f.FinishInflightUtterance(cookie);

      using(f.OpenScope("for")) {
        var loopIndex=f.Declare.Int("loopIndex");
        initialize(loopIndex);
        f.While(condition(loopIndex))
          .Do(() => {
            body(loopIndex);
            increment(loopIndex);
          });
      }
    }
  }
}
