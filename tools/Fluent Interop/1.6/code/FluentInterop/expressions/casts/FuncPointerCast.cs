using System;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public sealed class FuncPointerCast : FuncPointer {
    private readonly Expression inner;

    public FuncPointerCast(Expression inner) {
      this.inner=inner;
    }

    protected override IReadable EvaluateToHelper(IReference storage) {
      return inner.EvaluateTo(storage);
    }

    public override bool HasConflictWith(IReference storage) {
      return inner.HasConflictWith(storage);
    }
  }
}
