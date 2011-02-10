using FluentInterop.CodeGeneration;
using FluentInterop.Expressions.Variables;
using FluentInterop.Fluent;

namespace FluentInterop.Expressions.Pio {
  public class FastOutputPort {
    private PIOReferenceVariable pio;
    private IntVariable bitmask;

    public FastOutputPort(PIOReferenceVariable pio, IntVariable bitmask) {
      this.pio=pio;
      this.bitmask=bitmask;
    }

    public void Initialize(MethodDispatchTablePointer firmware, IntExpression pinNumber, bool initialValue) {
      firmware.GetPIOAndBitmask(pinNumber, ref pio, ref bitmask);
      pio.PER=bitmask; //enable PIO function
      if(initialValue) {
        pio.SODR=bitmask; //output should start high
      } else {
        pio.CODR=bitmask; //output should start low
      }
      pio.OER=bitmask; //enable output
    }

    public void Set() {
      pio.SODR=bitmask;
    }

    public void Clear() {
      pio.CODR=bitmask;
    }

    public void Write(IntExpression value) {
      var f=FuncBuilder.Instance;
      f.If(value!=0)
        .Then(Set)
        .Else(Clear)
        .Endif();
    }
  }
}
