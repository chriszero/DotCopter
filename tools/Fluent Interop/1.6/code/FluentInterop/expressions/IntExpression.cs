using System;
using System.Runtime.CompilerServices;
using FluentInterop.BoolExpressions;
using FluentInterop.OpCodes;

namespace FluentInterop.Expressions {
  public abstract class IntExpression : Expression {
    public static implicit operator IntExpression(int value) {
      return new IntConstant(value);
    }
    public static BoolExpression operator==(IntExpression lhs, IntExpression rhs) {
      return CompareOp.CreateEqual(lhs, rhs);
    }
    public static BoolExpression operator!=(IntExpression lhs, IntExpression rhs) {
      return CompareOp.CreateNotEqual(lhs, rhs);
    }
    public static BoolExpression operator<(IntExpression lhs, IntExpression rhs) {
      return CompareOp.CreateLessThan(lhs, rhs);
    }
    public static BoolExpression operator<=(IntExpression lhs, IntExpression rhs) {
      return CompareOp.CreateLessThanOrEqual(lhs, rhs);
    }
    public static BoolExpression operator>(IntExpression lhs, IntExpression rhs) {
      return CompareOp.CreateLessThan(rhs, lhs); //(a>b) is the same as (b<a)
    }
    public static BoolExpression operator>=(IntExpression lhs, IntExpression rhs) {
      return CompareOp.CreateLessThanOrEqual(rhs, lhs); //(a>=b) is the same as (b<=a)
    }
    public static IntExpression operator+(IntExpression lhs, IntExpression rhs) {
      return Add.Create(lhs, rhs);
    }
    public static IntExpression operator*(IntExpression lhs, IntExpression rhs) {
      return new AluOperation(lhs, rhs, Format4OpCode.MUL);
    }
    public static IntExpression operator-(IntExpression lhs, IntExpression rhs) {
      return Add.Create(lhs, -rhs);
    }
    public static IntExpression operator-(IntExpression expr) {
      return UnaryMinus.Create(expr);
    }
    public static IntExpression operator&(IntExpression lhs, IntExpression rhs) {
      return new AluOperation(lhs, rhs, Format4OpCode.AND);
    }
    public static IntExpression operator|(IntExpression lhs, IntExpression rhs) {
      return new AluOperation(lhs, rhs, Format4OpCode.ORR);
    }
    public static IntExpression operator^(IntExpression lhs, IntExpression rhs) {
      return new AluOperation(lhs, rhs, Format4OpCode.EOR);
    }

    /// <summary>
    /// operator&lt;&lt; not overridable in the way I would like
    /// </summary>
    public IntExpression ShiftLeft(IntExpression rhs) {
      return Shift.Create(this, rhs, true);
    }
    public IntExpression ShiftRight(IntExpression rhs) {
      return Shift.Create(this, rhs, false);
    }

    /// <summary>
    /// There are overrides at IntConstant and at UnaryMinus
    /// </summary>
    public virtual bool IsNegated {
      get { return false; }
    }

    /// <summary>
    /// Define this so compiler stops warning about it
    /// </summary>
    public override bool Equals(object obj) {
      return base.Equals(obj);
    }

    public override int GetHashCode() {
      return base.GetHashCode();
    }
  }
}
