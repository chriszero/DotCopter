using FluentInterop.CodeGeneration.Entities;

namespace FluentInterop.BoolExpressions {
  public abstract class BoolExpression {
    public static BoolExpression operator&(BoolExpression lhs, BoolExpression rhs) {
      return new And(lhs, rhs);
    }
    public static BoolExpression operator|(BoolExpression lhs, BoolExpression rhs) {
      return new Or(lhs, rhs);
    }

    public static bool operator true(BoolExpression expr) {
      //don't allow shortcutting
      return false;
    }
    public static bool operator false(BoolExpression expr) {
      //don't allow shortcutting
      return false;
    }

    public static BoolExpression operator!(BoolExpression expr) {
      return expr.Invert();
    }

    protected abstract BoolExpression Invert();
    public abstract void BranchIf(bool sense, Label target);
  }
}
