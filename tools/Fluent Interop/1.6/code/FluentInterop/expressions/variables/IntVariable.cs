using FluentInterop.Expressions.Pointers;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public sealed class IntVariable : IntExpression, IReference {
    protected override IReadable EvaluateToHelper(IReference storage) {
      return this;
    }

    public override bool HasConflictWith(IReference storage) {
      return ReferenceEquals(this, storage);
    }

    public IntExpression Value {
      set {
        Assignment.AssignAny(this, value);
      }
    }

    public IntPointer GetAddress() {
      return AddressOf.FromStaticVariable(this).AsIntPointer();
    }
  }
}