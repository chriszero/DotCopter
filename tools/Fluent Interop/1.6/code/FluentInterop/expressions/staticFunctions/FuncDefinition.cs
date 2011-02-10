using FluentInterop.CodeGeneration;

namespace FluentInterop.Expressions.StaticFunctions {
  public class FuncDefinition {
    public readonly FuncDeclaration Declaration;
    public readonly ActionOnFuncBuilder Body;

    public FuncDefinition(FuncDeclaration declaration, ActionOnFuncBuilder body) {
      this.Declaration=declaration;
      this.Body=body;
    }
  }
}
