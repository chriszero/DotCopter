using FluentInterop.CodeGeneration;

namespace FluentInterop.Representations {
  public sealed class StackWordRelativeToEndOfVariables : StackWord {
    private readonly int offset;

    public StackWordRelativeToEndOfVariables(int offset) {
      this.offset=offset;
    }

    public override bool EqualTo(Representation other) {
      var otherSw=other as StackWordRelativeToEndOfVariables;
      return otherSw!=null && this.offset==otherSw.offset;
    }

    protected override int CalculateOffset() {
      var f=FuncBuilder.Instance;
      return f.EndOfVariableRegion+this.offset-f.StackPointer;
    }

    public override string ToString() {
      //offset is always negative so this looks like "endVars-4"
      return "endVars"+offset;
    }
  }
}
