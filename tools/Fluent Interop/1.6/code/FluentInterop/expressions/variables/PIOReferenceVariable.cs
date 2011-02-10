using FluentInterop.Expressions.Pio;
using FluentInterop.Representations;

namespace FluentInterop.Expressions.Variables {
  public class PIOReferenceVariable : PIOReference, IReference {
    protected override IReadable EvaluateToHelper(IReference storage) {
      return this;
    }

    public override bool HasConflictWith(IReference storage) {
      return ReferenceEquals(this, storage);
    }
  }
}
