using FluentInterop.CodeGeneration.Entities;
using FluentInterop.Representations;

namespace FluentInterop.CodeGeneration.Util {
  public sealed class LocalVariableInfo : VariableInfo {
    public LocalVariableInfo(TerminalName name, Representation representation) : base(name, representation) {}
  }
}
