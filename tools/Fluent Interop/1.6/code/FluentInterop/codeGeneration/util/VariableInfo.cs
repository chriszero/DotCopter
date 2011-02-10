using FluentInterop.CodeGeneration.Entities;
using FluentInterop.Representations;

namespace FluentInterop.CodeGeneration.Util {
  public abstract class VariableInfo {
    public readonly TerminalName Name;
    public readonly Representation Representation;

    protected VariableInfo(TerminalName name, Representation representation) {
      this.Name=name;
      this.Representation=representation;
    }
  }
}
