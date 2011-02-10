using FluentInterop.BoolExpressions;
using FluentInterop.CodeGeneration;
using FluentInterop.Fluent;

namespace FluentInterop.CodeGeneration {
  partial class FuncBuilder {
    public If If(BoolExpression condition) {
      return new If(condition);
    }
  }
}

namespace FluentInterop.Fluent {
  public sealed class If {
    private readonly BoolExpression condition;
    private readonly object cookie;

    public If(BoolExpression condition) {
      this.condition=condition;
      this.cookie=FuncBuilder.Instance.StartInflightUtterance("If requires an Endif");
    }

    public WithOptionalElse Then(Action thenAction) {
      return new WithOptionalElse(condition, thenAction, null, cookie);
    }
  }

  public class Then {
    protected readonly BoolExpression condition;
    protected readonly Action thenAction;
    private readonly Action elseAction;
    protected readonly object cookie;

    public Then(BoolExpression condition, Action thenAction, Action elseAction, object cookie) {
      this.condition=condition;
      this.thenAction=thenAction;
      this.elseAction=elseAction;
      this.cookie=cookie;
    }

    public void Endif() {
      var f=FuncBuilder.Instance;
      f.FinishInflightUtterance(cookie);
      using(f.OpenScope("if")) {
        var elseLabel=f.DeclareLabel("else");
        var endifLabel=f.DeclareLabel("endif");
        condition.BranchIf(false, elseLabel);
        using(f.OpenScope("truePart")) {
          thenAction();
          if(elseAction!=null) {
            BranchLogic.UnconditionalBranchTo(endifLabel);
          }
        }
        using(f.OpenScope("falsePart")) {
          elseLabel.Mark();
          if(elseAction!=null) {
            elseAction();
          }
        }
        endifLabel.Mark();
      }
    }
  }

  public sealed class WithOptionalElse : Then {
    public WithOptionalElse(BoolExpression condition, Action thenAction, Action elseAction, object cookie)
      : base(condition, thenAction, elseAction, cookie) { }

    public Then Else(Action elseAction) {
      return new Then(condition, thenAction, elseAction, cookie);
    }
  }
}
