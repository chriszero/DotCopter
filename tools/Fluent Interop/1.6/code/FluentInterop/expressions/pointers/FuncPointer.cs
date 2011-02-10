using FluentInterop.CodeGeneration;

namespace FluentInterop.Expressions {
  public abstract class FuncPointer : Pointer {
    public Invoker BeginVoidFunctionCall {
      get { return new Invoker(this, null); }
    }

    public Invoker BeginFunctionCallWithResult(IntVariable result) {
      return new Invoker(this, result);
    }
  }

  public class Invoker {
    private readonly FuncPointer funcPointer;
    private readonly IntVariable result;
    private readonly object cookie;

    public Invoker(FuncPointer funcPointer, IntVariable result) {
      this.funcPointer=funcPointer;
      this.result=result;
      this.cookie=FuncBuilder.Instance.StartInflightUtterance("BeginFunctionCall needs to be followed by Invoke");
    }

    public void Invoke(IntExpression intParam0=null, IntExpression intParam1=null, IntExpression intParam2=null, IntExpression intParam3=null,
      BytePointer bytePtrParam4=null, BytePointer bytePtrParam5=null, BytePointer bytePtrParam6=null, BytePointer bytePtrParam7=null,
      IntPointer intPtrParam8=null, IntPointer intPtrParam9=null, IntPointer intPtrParam10=null, IntPointer intPtrParam11=null,
      FuncPointer funcPtrParam12=null, FuncPointer funcPtrParam13=null, FuncPointer funcPtrParam14=null, FuncPointer funcPtrParam15=null,
      MethodDispatchTablePointer firmwareParam16=null) {

      this.Invoke(new Expression[] {
        intParam0, intParam1, intParam2, intParam3,
        bytePtrParam4, bytePtrParam5, bytePtrParam6, bytePtrParam7,
        intPtrParam8, intPtrParam9, intPtrParam10, intPtrParam11,
        funcPtrParam12, funcPtrParam13, funcPtrParam14, funcPtrParam15
      });
    }

    public void Invoke(params Expression[] arguments) {
      var f=FuncBuilder.Instance;
      f.FinishInflightUtterance(cookie);
      f.Invoke(funcPointer, result, arguments);
    }
  }
}
