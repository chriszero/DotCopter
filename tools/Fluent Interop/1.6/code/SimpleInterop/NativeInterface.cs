using System.Runtime.CompilerServices;

namespace Kosak.SimpleInterop {
  public static class NativeInterface {
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetInfo(int query);

    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int Execute(short[] code,
      int i0, int i1, int i2, int i3,
      byte[] ba0, byte[] ba1, byte[] ba2, byte[] ba3,
      int[] ia0, int[] ia1, int[] ia2, int[] ia3,
      short[] sa0, short[] sa1, short[] sa2, short[] sa3);
  }
}
