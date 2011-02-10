using FluentInterop.CodeGeneration;
using FluentInterop.Expressions;
using FluentInterop.Fluent;

namespace FluentInterop.CodeGeneration {
  partial class FuncBuilder {
    public StandardArgs StandardArgs { get { return new StandardArgs(); } }
  }
}

namespace FluentInterop.Fluent {
  public sealed class StandardArgs {
    public IntVariable intParam0 { get { return IntParam(0); } }
    public IntVariable intParam1 { get { return IntParam(1); } }
    public IntVariable intParam2 { get { return IntParam(2); } }
    public IntVariable intParam3 { get { return IntParam(3); } }
    public BytePointerVariable bytePtrParam4 { get { return BytePointerParam(4); } }
    public BytePointerVariable bytePtrParam5 { get { return BytePointerParam(5); } }
    public BytePointerVariable bytePtrParam6 { get { return BytePointerParam(6); } }
    public BytePointerVariable bytePtrParam7 { get { return BytePointerParam(7); } }
    public IntPointerVariable intPtrParam8 { get { return IntPtrParam(8); } }
    public IntPointerVariable intPtrParam9 { get { return IntPtrParam(9); } }
    public IntPointerVariable intPtrParam10 { get { return IntPtrParam(10); } }
    public IntPointerVariable intPtrParam11 { get { return IntPtrParam(11); } }
    public FuncPointerVariable funcPtrParam12 { get { return FuncPtrParam(12); } }
    public FuncPointerVariable funcPtrParam13 { get { return FuncPtrParam(13); } }
    public FuncPointerVariable funcPtrParam14 { get { return FuncPtrParam(14); } }
    public FuncPointerVariable funcPtrParam15 { get { return FuncPtrParam(15); } }
    public MethodDispatchTableVariable firmwareParam16 { get { return Argument(16).MethodDispatchTable("firmwareParam16"); } }

    public static IntVariable IntParam(int index) {
      return Argument(index).Int("intParam"+index);
    }
    public static BytePointerVariable BytePointerParam(int index) {
      return Argument(index).BytePointer("bytePtrParam"+index);
    }
    public static IntPointerVariable IntPtrParam(int index) {
      return Argument(index).IntPointer("intPtrParam"+index);
    }
    public static FuncPointerVariable FuncPtrParam(int index) {
      return Argument(index).FuncPointer("funcPtrParam"+index);
    }
    public static ParameterDeclaration Argument(int index) {
      return FuncBuilder.Instance.Declare.Argument(index);
    }
  }
}
