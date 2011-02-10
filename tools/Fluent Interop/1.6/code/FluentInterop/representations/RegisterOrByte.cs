namespace FluentInterop.Representations {
  public class RegisterOrByte {
    public static RegisterOrByte Create(LowRegister register) {
      return new RegisterOrByte(register, 0);
    }
    public static RegisterOrByte Create(byte value) {
      return new RegisterOrByte(null, value);
    }

    public readonly LowRegister Register;
    public readonly byte Byte;

    public RegisterOrByte(LowRegister register, byte b) {
      Register=register;
      Byte=b;
    }

    public bool IsRegister {
      get { return Register!=null; }
    }
  }
}
