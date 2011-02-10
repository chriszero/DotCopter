using FluentInterop.CodeGeneration;
using FluentInterop.Expressions.Variables;

namespace FluentInterop.Expressions {
  public abstract class MethodDispatchTablePointer : IntPointer {
    public void EnableOutputPin(IntExpression pin, IntExpression initialState) {
      InvokeVoid(4, pin, initialState);
    }

    public void EnableInputPin(IntExpression pin, IntExpression glitchFilterEnable,
      FuncPointer pinIsr, IntExpression intEdge, IntExpression resistorState) {
      InvokeVoid(5, pin, glitchFilterEnable, pinIsr, intEdge, resistorState);
    }

    public void EnableInputPin2(IntExpression pin, IntExpression glitchFilterEnable,
      FuncPointer pinIsr, IntExpression payload, IntExpression intEdge, IntExpression resistorState) {
      InvokeVoid(6, pin, glitchFilterEnable, pinIsr, payload, intEdge, resistorState);
    }

    public void GetPinState(ref IntVariable result, IntExpression pin) {
      InvokeResult(7, result, pin);
    }

    public void SetPinState(IntExpression pin, IntExpression value) {
      InvokeVoid(8, pin, value);
    }

    /// <summary>
    /// HACK: GetMachineTime actually returns a long. We are going to look at just the low word for now
    /// </summary>
    /// <param name="result"></param>
    public void GetLowWordOfMachineTime(ref IntVariable result) {
      InvokeResult(17, result);
    }

    public void IntegerDivide(ref IntVariable result, IntExpression numerator, IntExpression denominator) {
      InvokeResult(18, result, numerator, denominator);
    }

    public void GetPIOReference(ref PIOReferenceVariable pio, IntExpression port) {
      //ugh.. fix this type system disaster
      var f=FuncBuilder.Instance;
      using(f.OpenScope("GetPIORef")) {
        var temp=f.Declare.Int("temp");
        InvokeResult(19, temp, port);
        Assignment.AssignAny(pio, temp); //escape from the type system
      }
    }

    public void HAL_InitializeForISR(IntPointer address, FuncPointer isr, Pointer arg) {
      InvokeVoid(20, address, isr, arg);
    }

    public void HAL_EnqueueDelta(IntPointer address, IntExpression delayInMicroseconds) {
      InvokeVoid(21, address, delayInMicroseconds);
    }

    public void HAL_Abort(IntPointer address) {
      InvokeVoid(22, address);
    }

    private void InvokeResult(int offset, IntVariable result, params Expression[] args) {
      this[offset].AsFuncPointer().BeginFunctionCallWithResult(result).Invoke(args);
    }

    private void InvokeVoid(int offset, params Expression[] args) {
      this[offset].AsFuncPointer().BeginVoidFunctionCall.Invoke(args);
    }
  }
}
