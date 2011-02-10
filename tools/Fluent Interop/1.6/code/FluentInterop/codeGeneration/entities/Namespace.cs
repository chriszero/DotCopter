using System.Collections;
using FluentInterop.Coding;

namespace FluentInterop.CodeGeneration.Entities {
  public sealed class Namespace {
    public readonly Namespace Inner;
    public readonly int Index;
    private readonly string humanReadable;
    public readonly int Depth;
    private readonly int cachedHashCode;

    public Namespace(Namespace inner, int index, string humanReadable) {
      this.Inner=inner;
      this.Index=index;
      this.humanReadable=humanReadable;
      if(inner==null) {
        this.Depth=0;
        this.cachedHashCode=index;
      } else {
        this.Depth=inner.Depth+1;
        this.cachedHashCode=(inner.cachedHashCode*397)^index;
      }
    }

    /// <summary>
    /// Written in a non-recursive fashion due to my fears of running out of stack space
    /// </summary>
    public override bool Equals(object obj) {
      var other=obj as Namespace;
      var lhs=this;
      var rhs=other;
      while(lhs!=null && rhs!=null) {
        if(lhs.Index!=rhs.Index || lhs.Depth!=rhs.Depth) {
          return false;
        }
        lhs=lhs.Inner;
        rhs=rhs.Inner;
      }
      return lhs==null && rhs==null;
    }

    public override int GetHashCode() {
      return cachedHashCode;
    }

    public override string ToString() {
      var items=new ArrayList();
      ToReversedElements(items);
      return items.ReverseConcat();
    }

    /// <summary>
    /// Written in a non-recursive fashion due to my fears of running out of stack space
    /// </summary>
    public void ToReversedElements(ArrayList items) {
      var next=this;
      while(true) {
        items.Add(next.humanReadable);
        items.Add("`");
        items.Add(next.Index.ToString());
        next=next.Inner;
        if(next==null) {
          break;
        }
        items.Add(".");
      }
    }
  }
}
