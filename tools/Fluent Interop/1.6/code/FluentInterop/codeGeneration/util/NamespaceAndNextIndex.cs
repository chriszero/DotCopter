using FluentInterop.CodeGeneration.Entities;

namespace FluentInterop.CodeGeneration.Util {
  public sealed class NamespaceAndNextIndex {
    public readonly Namespace Namespace;
    public readonly int NextIndex;

    public NamespaceAndNextIndex(Namespace ns, int nextIndex) {
      this.Namespace=ns;
      this.NextIndex=nextIndex;
    }

    public NamespaceAndNextIndex Nest(string newName) {
      var inner=new Namespace(Namespace, NextIndex, newName);
      return new NamespaceAndNextIndex(inner, 0);
    }

    public NamespaceAndNextIndex UnNest() {
      return new NamespaceAndNextIndex(Namespace.Inner, this.Namespace.Index+1);
    }
  }
}
