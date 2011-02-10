using FluentInterop.CodeGeneration;
using FluentInterop.Expressions;
using FluentInterop.Expressions.Pio;
using FluentInterop.Expressions.Variables;
using FluentInterop.Representations;

namespace FluentInterop.Fluent {
  public class Declaration {
    public IntVariable Int(string name, IntExpression optionalInitialValue=null) {
      return (IntVariable)DeclareHelper(name, new IntVariable(), optionalInitialValue);
    }

    public BytePointerVariable BytePointer(string name, BytePointer optionalInitialValue=null) {
      return (BytePointerVariable)DeclareHelper(name, new BytePointerVariable(), optionalInitialValue);
    }

    public IntPointerVariable IntPointer(string name, IntPointer optionalInitialValue=null) {
      return (IntPointerVariable)DeclareHelper(name, new IntPointerVariable(), optionalInitialValue);
    }

    public FuncPointerVariable FuncPointer(string name, FuncPointer optionalInitialValue=null) {
      return (FuncPointerVariable)DeclareHelper(name, new FuncPointerVariable(), optionalInitialValue);
    }

    public MethodDispatchTableVariable MethodDispatchTable(string name, MethodDispatchTablePointer optionalInitialValue=null) {
      return (MethodDispatchTableVariable)DeclareHelper(name, new MethodDispatchTableVariable(), optionalInitialValue);
    }

    public PIOReferenceVariable PIOReference(string name) {
      return (PIOReferenceVariable)DeclareHelper(name, new PIOReferenceVariable(), null);
    }

    public FastOutputPort FastOutputPort(string name) {
      var pioReference=PIOReference(name+"_PIO");
      var bitmask=Int(name+"_bitmask");
      return new FastOutputPort(pioReference, bitmask);
    }

    protected virtual IReference DeclareHelper(string name, IReference variable, Expression optionalInitialValue) {
      FuncBuilder.Instance.BindLocalVariableToRepresentation(variable, name);
      if(!ReferenceEquals(optionalInitialValue, null)) {
        Assignment.AssignAny(variable, optionalInitialValue);
      }
      return variable;
    }
  }
}
