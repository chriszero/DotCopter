using FluentInterop.CodeGeneration;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public sealed class IntPointerVariable : IntPointer, IReference {
    protected override IReadable EvaluateToHelper(IReference storage) {
      return this;
    }

    public override bool HasConflictWith(IReference storage) {
      return ReferenceEquals(this, storage);
    }

    public IntPointer Value {
      set {
        Assignment.AssignAny(this, value);
      }
    }
  }
}
