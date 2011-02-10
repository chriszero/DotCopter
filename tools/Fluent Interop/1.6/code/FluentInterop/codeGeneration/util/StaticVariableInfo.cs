using FluentInterop.CodeGeneration.Entities;
using FluentInterop.Representations;

namespace FluentInterop.CodeGeneration.Util {
  public sealed class StaticVariableInfo : VariableInfo {
    public StaticVariableInfo(TerminalName name, Static representation) : base(name, representation) {}

    new public Static Representation {
      get { return (Static)base.Representation; }
    }
  }
}
