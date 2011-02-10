using System;
using FluentInterop.Expressions.Pointers;
using FluentInterop.Representations;

namespace FluentInterop.Expressions.Arrays {
  public sealed class IntArrayVariable : IntPointer, IReference {
    protected override IReadable EvaluateToHelper(IReference storage) {
      return AddressOf.FromStaticVariable(this).EvaluateTo(storage);
    }

    public override bool HasConflictWith(IReference storage) {
      return storage is Pointer;
    }
  }
}
