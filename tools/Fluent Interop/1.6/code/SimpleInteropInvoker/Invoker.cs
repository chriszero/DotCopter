using System;

namespace Kosak.SimpleInterop {
  public class CompiledCode {
    private const int expectedVersionNumber=1007000;
    public const int SizeofHalCompletion=32;

    private static bool checkedVersion;

    public readonly short[] OpCodes;

    public CompiledCode(short[] opCodes) {
      this.OpCodes=opCodes;
    }

    public int Invoke(AllArguments arguments=null) {
      var a=arguments ?? new AllArguments();
      return Invoke(a.intParam0, a.intParam1, a.intParam2, a.intParam3,
        a.bytePtrParam4, a.bytePtrParam5, a.bytePtrParam6, a.bytePtrParam7,
        a.intPtrParam8, a.intPtrParam9, a.intPtrParam10, a.intPtrParam11,
        a.funcPtrParam12, a.funcPtrParam13, a.funcPtrParam14, a.funcPtrParam15);
    }

    public int Invoke(int intParam0=0, int intParam1=0, int intParam2=0, int intParam3=0,
      byte[] byteArrayParam4=null, byte[] byteArrayParam5=null, byte[] byteArrayParam6=null, byte[] byteArrayParam7=null,
      int[] intArrayParam8=null, int[] intArrayParam9=null, int[] intArrayParam10=null, int[] intArrayParam11=null,
      CompiledCode ccParam12=null, CompiledCode ccParam13=null, CompiledCode ccParam14=null, CompiledCode ccParam15=null) {

      if(!checkedVersion) {
        var versionNumber=NativeInterface.GetInfo(0);
        if(versionNumber!=expectedVersionNumber) {
          throw new Exception("version number mismatch");
        }
        checkedVersion=true;
      }

      return NativeInterface.Execute(OpCodes,
        intParam0, intParam1, intParam2, intParam3,
        byteArrayParam4, byteArrayParam5, byteArrayParam6, byteArrayParam7,
        intArrayParam8, intArrayParam9, intArrayParam10, intArrayParam11,
        GetArrayHelper(ccParam12), GetArrayHelper(ccParam13), GetArrayHelper(ccParam14), GetArrayHelper(ccParam15)
        );
    }

    private static short[] GetArrayHelper(CompiledCode cc) {
      return cc==null ? null : cc.OpCodes;
    }
  }
}
