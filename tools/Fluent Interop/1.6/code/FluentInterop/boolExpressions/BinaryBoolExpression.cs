namespace FluentInterop.BoolExpressions {
  public abstract class BinaryBoolExpression : BoolExpression {
    protected readonly BoolExpression lhs;
    protected readonly BoolExpression rhs;

    protected BinaryBoolExpression(BoolExpression lhs, BoolExpression rhs) {
      this.lhs=lhs;
      this.rhs=rhs;
    }
  }
}
