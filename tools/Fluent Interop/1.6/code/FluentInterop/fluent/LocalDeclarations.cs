using System;
using FluentInterop.CodeGeneration;
using FluentInterop.Expressions;
using FluentInterop.Fluent;
using FluentInterop.Representations;

namespace FluentInterop.CodeGeneration {
  partial class FuncBuilder {
    public DeclarationWithArgumentOption Declare { get { return new DeclarationWithArgumentOption(); } }
  }
}

namespace FluentInterop.Fluent {
  public class DeclarationWithArgumentOption : Declaration {
    public ParameterDeclaration Argument(int index) { return new ParameterDeclaration(index); }
  }

  public class ParameterDeclaration : Declaration {
    private readonly int parameterIndex;

    public ParameterDeclaration(int parameterIndex) {
      this.parameterIndex=parameterIndex;
    }

    protected override IReference DeclareHelper(string name, IReference variable, Expression optionalInitialValue) {
      if(!ReferenceEquals(optionalInitialValue,null)) {
        throw new Exception("initialization values don't make sense for parameters");
      }
      var representation=parameterIndex<4
        ? (Representation)new LowRegister(parameterIndex)
        : new StackWordRelativeToZero((parameterIndex-4)*4);
      var f=FuncBuilder.Instance;
      f.BindLocalVariableToRepresentation(variable, name, representation);
      return variable;
    }
  }
}
