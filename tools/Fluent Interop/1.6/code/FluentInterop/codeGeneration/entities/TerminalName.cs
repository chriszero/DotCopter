using System.Collections;
using FluentInterop.Coding;

namespace FluentInterop.CodeGeneration.Entities {
  public sealed class TerminalName {
    public readonly Namespace Namespace;
    private readonly string name;

    public TerminalName(Namespace ns, string name) {
      this.Namespace=ns;
      this.name=name;
    }

    public override bool Equals(object obj) {
      var other=obj as TerminalName;
      return other!=null
        && this.name==other.name
        && this.Namespace.Equals(other.Namespace);
    }

    public override int GetHashCode() {
      return Namespace.GetHashCode()*397^name.GetHashCode();
    }

    public override string ToString() {
      var items=new ArrayList {name, "::"};
      Namespace.ToReversedElements(items);
      return items.ReverseConcat();
    }
  }
}
