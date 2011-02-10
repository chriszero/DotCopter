using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public sealed class MethodDispatchTableVariable : MethodDispatchTablePointer, IReference {
    protected override IReadable EvaluateToHelper(IReference storage) {
      return this;
    }

    public override bool HasConflictWith(IReference storage) {
      return ReferenceEquals(this, storage);
    }

    public MethodDispatchTablePointer Value {
      set {
        Assignment.AssignAny(this, value);
      }
    }
  }
}
