#if false
using System;
using FluentInterop.CodeGeneration;
using FluentInterop.Expressions;

namespace FluentInterop.Representations {
  public sealed class ByteArrayElement : Representation {
    private readonly Expression array;
    private readonly Expression index;

    public ArrayElement(string name, ExpressionType expressionType, Expression array, Expression index) : base(name, expressionType) {
      this.array=array;
      this.index=index;
    }

    public override void ToSpecifiedRegister(ICodeGenerator g, Register result) {
      string opCode;
      string shifter;
      int multiplier;
      switch(array.ExpressionType) {
        case ExpressionType.WordArray:
          opCode="LDR";
          shifter=",LSL 2";
          multiplier=4;
          break;

        case ExpressionType.ByteArray:
          opCode="LDRB";
          shifter="";
          multiplier=1;
          break;

        default:
          throw new Exception("Can't index an expression of type "+array.ExpressionType);
      }

      throw new NotImplementedException("NIY");
//
//      array.LoadToNewRegister(g, arrayRegister =>
//        index.ToRegisterOrImmediate(g,
//          indexRegister => g.Emit(OpCodes.TODO, opCode+" "+result+",["+arrayRegister+",+"+indexRegister+shifter+"]"),
//          indexImmediate => g.Emit(OpCodes.TODO, opCode+" "+result+",["+arrayRegister+",+#"+indexImmediate*multiplier+"]")));
    }

    public override void FromSpecifiedRegister(ICodeGenerator g, Register register) {
      throw new NotImplementedException();
    }
  }
}
#endif
