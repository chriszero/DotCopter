using System;
using FluentInterop.CodeGeneration;
using FluentInterop.Expressions;
using FluentInterop.Expressions.Arrays;
using FluentInterop.Expressions.Firmware;
using FluentInterop.Expressions.StaticFunctions;
using FluentInterop.Fluent;
using FluentInterop.Representations;
using Kosak.SimpleInterop;


namespace FluentInterop.CodeGeneration {
  partial class CodeGenerator {
    public StaticKeywordNext Declare { get { return new StaticKeywordNext(); } }
  }
}

namespace FluentInterop.Fluent {
  public class StaticKeywordNext {
    public StaticDeclaration Static { get { return new StaticDeclaration(); } }

    public FuncDeclaration Function(string name, ActionOnFuncBuilder body=null) {
      var result=new FuncDeclaration(name);
      if(body!=null) {
        result.Define(body);
      }
      return result;
    }
  }

  public class StaticDeclaration : Declaration {
    protected override IReference DeclareHelper(string name, IReference variable, Expression optionalInitialValue) {
      var integerConstant=0;
      if(!ReferenceEquals(optionalInitialValue, null) && !optionalInitialValue.TryGetConstant(out integerConstant)) {
        throw new Exception("Sorry, only integer initializers are allowed for static constants");
      }
      CodeGenerator.Instance.BindStaticVariableToNewRepresentation(variable, name, new[] {integerConstant});
      return variable;
    }

    public IntArrayVariable IntArray(string name, int size) {
      return IntArray(name, new int[size]);
    }

    public IntArrayVariable IntArray(string name, params int[] values) {
      var variable=new IntArrayVariable();
      CodeGenerator.Instance.BindStaticVariableToNewRepresentation(variable, name, values);
      return variable;
    }

    public HalCompletion HalCompletion(string name) {
      return new HalCompletion(IntArray(name, CompiledCode.SizeofHalCompletion/4));
    }
  }
}
