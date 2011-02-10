using FluentInterop.CodeGeneration;
using FluentInterop.CodeGeneration.Entities;
using FluentInterop.Expressions.Pointers;
using FluentInterop.Representations;

namespace FluentInterop.Expressions.StaticFunctions {
  public class FuncDeclaration : FuncPointer {
    public readonly string Name;
    public readonly Label EntryPoint;

    public FuncDeclaration(string name) {
      this.Name=name;
      this.EntryPoint=CodeGenerator.Instance.DeclareStaticLabel(name);
    }

    public void Define(ActionOnFuncBuilder body) {
      CodeGenerator.Instance.AddFuncDefinition(new FuncDefinition(this, body));
    }

    protected override IReadable EvaluateToHelper(IReference storage) {
      return new AddressOf(EntryPoint, true).EvaluateTo(storage);
    }
  }
}
