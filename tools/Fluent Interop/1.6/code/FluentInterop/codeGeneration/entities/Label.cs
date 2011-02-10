namespace FluentInterop.CodeGeneration.Entities {
  public sealed class Label {
    public readonly TerminalName Name;

    public Label(TerminalName name) {
      this.Name=name;
    }

    public void Mark() {
      CodeGenerator.Instance.MarkLabel(Name);
    }

    public override string ToString() {
      return Name.ToString();
    }

    public int GetLabelAddressBestEffort() {
      return CodeGenerator.Instance.GetLabelAddressBestEffort(Name);
    }
  }
}
