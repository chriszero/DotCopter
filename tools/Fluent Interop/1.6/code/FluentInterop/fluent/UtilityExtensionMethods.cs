using FluentInterop.CodeGeneration;
using FluentInterop.Expressions;
using FluentInterop.Expressions.Variables;

namespace FluentInterop.Fluent {
  public static class UtilityExtensionMethods {
    public static void ForEachBit(this FuncBuilder f, IntExpression expr, int offset, int count, bool bigEndian, ActionOnIntExpr action) {
      int inclusiveStart;
      int exclusiveEnd;
      int increment;
      if(!bigEndian) {
        inclusiveStart=offset;
        exclusiveEnd=offset+count;
        increment=1;
      } else {
        inclusiveStart=offset+count-1;
        exclusiveEnd=offset-1;
        increment=-1;
      }

      f.For(i => i.Value=inclusiveStart, i => i<exclusiveEnd, i => i.Value=i+increment)
        .Do(i => {
          var mask=((IntExpression)1).ShiftLeft(i);
          var bitInPosition=f.Declare.Int("bitInPosition");
          bitInPosition.Value=expr&mask;
          action(bitInPosition);
        });
    }

    public static void GetPIOAndBitmask(this MethodDispatchTablePointer md, IntExpression pin, ref PIOReferenceVariable pio, ref IntVariable bitmask) {
      md.GetPIOReference(ref pio, pin.ShiftRight(5)); // pin/32
      bitmask.Value=((IntExpression)1).ShiftLeft(pin&0x1f); // 1<<(pin%32)
    }
  }
}
