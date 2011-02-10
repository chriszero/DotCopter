using FluentInterop.CodeGeneration;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public class BytePointerVariable : BytePointer, IReference {
    protected override IReadable EvaluateToHelper(IReference storage) {
      return this;
    }

    public override bool HasConflictWith(IReference storage) {
      return ReferenceEquals(this, storage);
    }

    public BytePointer Value {
      set {
        Assignment.AssignAny(this, value);
      }
    }
  }
}
