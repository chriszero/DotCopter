using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public sealed class IntPointerCast : IntPointer {
    private readonly Expression inner;

    public IntPointerCast(Expression inner) {
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
