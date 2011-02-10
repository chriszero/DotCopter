using FluentInterop.BoolExpressions;

namespace FluentInterop.Expressions {
  public abstract class Pointer : Expression {
    public BoolExpression IsNull {
      get { return this.AsInt()==0; }
    }
  }
}
