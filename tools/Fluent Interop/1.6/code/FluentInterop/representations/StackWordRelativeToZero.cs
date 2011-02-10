using FluentInterop.CodeGeneration;

namespace FluentInterop.Representations {
  public sealed class StackWordRelativeToZero : StackWord {
    private readonly int offset;

    public StackWordRelativeToZero(int offset) {
      this.offset=offset;
    }

    public override bool EqualTo(Representation other) {
      var otherSw=other as StackWordRelativeToZero;
      return otherSw!=null && this.offset==otherSw.offset;
    }

    protected override int CalculateOffset() {
      return offset-FuncBuilder.Instance.StackPointer;
    }

    public override string ToString() {
      //offset is generally positive, so this looks like "startSP+4"
      return "startSP+"+offset;
    }
  }
}
