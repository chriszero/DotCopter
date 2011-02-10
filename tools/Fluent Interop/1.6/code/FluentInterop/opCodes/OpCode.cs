namespace FluentInterop.OpCodes {
  public enum Format1OpCode {
    LSL=0,
    LSR=1,
    ASR=2,
  }

  public enum Format2OpCode {
    ADD=0,
    SUB=1,
  }

  public enum Format3OpCode {
    MOV=0,
    CMP=1,
    ADD=2,
    SUB=3,
  }

  public enum Format4OpCode {
    AND=0,
    EOR=1,
    LSL=2,
    LSR=3,
    ASR=4,
    ADC=5,
    SBC=6,
    ROR=7,
    TST=8,
    NEG=9,
    CMP=10,
    CMN=11,
    ORR=12,
    MUL=13,
    BIC=14,
    MVN=15,
  }

  public enum Format5OpCode {
    ADD=0,
    CMP=1,
    MOV=2,
    BX=3,
  }

  public enum Format6OpCode {
    LDR=-1, //this value is a placeholder because there is only one operation in this format
  }

  public enum Format7OpCode {
    STR=0,
    STRB=1,
    LDR=2,
    LDRB=3,
  }

  public enum Format8OpCode {
    STRH=0,
    LDSB=1,
    LDRH=2,
    LDSH=3,
  }

  public enum Format9OpCode {
    STR=0,
    LDR=1,
    STRB=2,
    LDRB=3,
  }

  public enum Format11OpCode {
    STR=0,
    LDR=1,
  }

  public enum Format12OpCode {
    LDADDR_PC=0,
    LDADDR_SP=1,
  }

  public enum Format13OpCode {
    ADDSP=-1, //this value is a placeholder because there is only one operation in this format
  }

  public enum Format14OpCode {
    PUSH=0,
    POP=1,
  }

  public enum Format16OpCode {
    BEQ=0,
    BNE=1,
    BCS=2,
    BCC=3,
    BMI=4,
    BPL=5,
    BVS=6,
    BVC=7,
    BHI=8,
    BLS=9,
    BGE=10,
    BLT=11,
    BGT=12,
    BLE=13,
  }

  public enum Format18OpCode {
    B=-1, //this value is a placeholder because there is only one operation in this format
  }

  public enum Format19OpCode {
    BL=-1, //this value is a placeholder because there is only one operation in this format
  }

  public static class HumanReadableExtensionMethods {
    public static string ToHumanReadable(this Format1OpCode opCode) {
      return "LSLLSRASR".Substring((int)opCode*3, 3);
    }
    public static string ToHumanReadable(this Format2OpCode opCode) {
      return "ADDSUB".Substring((int)opCode*3, 3);
    }
    public static string ToHumanReadable(this Format3OpCode opCode) {
      return "MOVCMPADDSUB".Substring((int)opCode*3, 3);
    }
    public static string ToHumanReadable(this Format4OpCode opCode) {
      return "ANDEORLSLLSRASRADCSBCRORTSTNEGCMPCMNORRMULBICMVN".Substring((int)opCode*3, 3);
    }
    public static string ToHumanReadable(this Format5OpCode opCode) {
      return "ADDCMPMOVBX ".Substring((int)opCode*3, 3);
    }
    private static readonly string[] format7=new[] { "STR", "STRB", "LDR", "LDRB" };
    public static string ToHumanReadable(this Format7OpCode opCode) {
      return format7[(int)opCode];
    }
    public static string ToHumanReadable(this Format8OpCode opCode) {
      return "STRHLDSBLDRHLDSH".Substring((int)opCode*4, 4);
    }
    private static readonly string[] format9=new[] { "STR", "LDR", "STRB", "LDRB" };
    public static string ToHumanReadable(this Format9OpCode opCode) {
      return format9[(int)opCode];
    }
    public static string ToHumanReadable(this Format11OpCode opCode) {
      return "STRLDR".Substring((int)opCode*3, 3);
    }
    public static string ToHumanReadable(this Format12OpCode opCode) {
      return "LDADDR_PCLDADDR_SP".Substring((int)opCode*9, 9);
    }
    private static readonly string[] format14=new[] { "PUSH", "POP" };
    public static string ToHumanReadable(this Format14OpCode opCode) {
      return format14[(int)opCode];
    }
    public static string ToHumanReadable(this Format16OpCode opCode) {
      return "BEQBNEBCSBCCBMIBPLBVSBVCBHIBLSBGEBLTBGTBLE".Substring((int)opCode*3, 3);
    }
  }
}
