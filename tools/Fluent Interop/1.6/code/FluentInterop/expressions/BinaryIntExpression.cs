using System;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public abstract class BinaryIntExpression : IntExpression {
    protected readonly IntExpression lhs;
    protected readonly IntExpression rhs;

    protected BinaryIntExpression(IntExpression lhs, IntExpression rhs) {
      this.lhs=lhs;
      this.rhs=rhs;
    }

    public override bool HasConflictWith(IReference storage) {
      return lhs.HasConflictWith(storage) || rhs.HasConflictWith(storage);
    }
  }
}
