using FluentInterop.Expressions.Arrays;

namespace FluentInterop.Expressions.Firmware {
  public class HalCompletion {
    private readonly IntArrayVariable storage;

    public HalCompletion(IntArrayVariable storage) {
      this.storage=storage;
    }

    public void InitializeForISR(MethodDispatchTablePointer firmware, FuncPointer isr) {
      firmware.HAL_InitializeForISR(storage, isr, ((IntExpression)0).AsIntPointer());
    }

    public void EnqueueDelta(MethodDispatchTablePointer firmware, IntExpression delayInMicroseconds) {
      firmware.HAL_EnqueueDelta(storage, delayInMicroseconds);
    }

    public void Abort(MethodDispatchTablePointer firmware) {
      firmware.HAL_Abort(storage);
    }
  }
}
