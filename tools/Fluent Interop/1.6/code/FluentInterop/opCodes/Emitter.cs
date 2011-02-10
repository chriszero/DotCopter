using System;
using System.Collections;
using FluentInterop.Coding;
using FluentInterop.Representations;
using Microsoft.SPOT;
using Math = System.Math;

namespace FluentInterop.OpCodes {
  public class Emitter {
    public int RegistersWritten;
    private readonly MyShortBuilder opCodes=new MyShortBuilder();

    public int CurrentAddress {
      get { return opCodes.Count*2; }
    }

    public void Emit(short opCode, string fluentComment, string assemblerComment) {
      Dump(opCode, fluentComment, assemblerComment);
      opCodes.Add(opCode);
    }

    public void Dump(short opCode, string fluentComment, string assemblerComment) {
      const int fluentWidth=40;
      var message="      ".MyConcat(((short)CurrentAddress).ToHex(), ": ", opCode.ToHex(), "  ", fluentComment.PaddedTo(fluentWidth)
        , "  ", assemblerComment);
      Debug.Print(message);
    }

    public short[] MakeOpCodes() {
      AlignToWord();
      return opCodes.ToShortArray();
    }

    public void AlignToWord() {
      if((CurrentAddress&2)!=0) {
        EmitPlaceholder("Alignment");
      }
    }

    public void Emit(Format1OpCode opCode, LowRegister rd, LowRegister rs, byte immediate) {
      CheckRange(immediate, 0, 31);
      var fluentComment=opCode.ToHumanReadable().MyConcat(" ", rd, ",", rs, ",#", immediate);
      EmitHelper(rd, 1, fluentComment, 0, 3, (int)opCode, 2, immediate, 5, rs.Index, 3, rd.Index, 3);
    }

    public void Emit(Format2OpCode opCode, LowRegister rd, LowRegister rs, LowRegister rn) {
      Format2Helper(opCode, 0, rd, rs, rn.Index, rn.ToString());
    }

    public void Emit(Format2OpCode opCode, LowRegister rd, LowRegister rs, byte immediate3Bit) {
      CheckRange(immediate3Bit, 0, 7);
      Format2Helper(opCode, 1, rd, rs, immediate3Bit, "#"+immediate3Bit);
    }

    private void Format2Helper(Format2OpCode opCode, int immediateBit,
      LowRegister rd, LowRegister rs, int rnBits, string suffix) {
      var fluentComment=opCode.ToHumanReadable().MyConcat(" ", rd, ",", rs, ",", suffix);
      EmitHelper(rd, 2, fluentComment, 3, 5, immediateBit, 1, (int)opCode, 1, rnBits, 3, rs.Index, 3, rd.Index, 3);
    }

    public void Emit(Format3OpCode opCode, LowRegister rd, byte value) {
      var fluentComment=opCode.ToHumanReadable().MyConcat(" ", rd, ",#", value);
      EmitHelper(rd, 3, fluentComment, 1, 3, (int)opCode, 2, rd.Index, 3, value, 8);
    }

    public void Emit(Format4OpCode opCode, LowRegister rd, LowRegister rs) {
      var fluentComment=opCode.ToHumanReadable().MyConcat(" ", rd, ",", rs);
      EmitHelper(rd, 4, fluentComment, 16, 6, (int)opCode, 4, rs.Index, 3, rd.Index, 3);
    }

    public void Emit(Format5OpCode opCode, Register rd, Register rs, string nopExplanation=null) {
      string fluentComment;
      var rsIsHigh=rs is HighRegister;
      var rdIsHigh=rd is HighRegister;
      if(opCode==Format5OpCode.BX) {
        //rd is ignored on BX instruction
        rd=Register.R0;
        fluentComment="BX "+rs;
      } else if(!rsIsHigh && !rdIsHigh) {
        throw new Exception("illegal use of this instruction");
      } else if(rd.EqualTo(rs)) {
        fluentComment="NOP";
      } else {
        fluentComment=opCode.ToHumanReadable().MyConcat(" ", rd, ",", rs);
      }
      if(nopExplanation!=null) {
        fluentComment=fluentComment+" ("+nopExplanation+")";
      }
      var maskedRs=rs.Index&7;
      var maskedRd=rd.Index&7;
      var h1Bits=rdIsHigh ? 1 : 0;
      var h2Bits=rsIsHigh ? 1 : 0;
      EmitHelper(rd, 5, fluentComment, 17, 6, (int)opCode, 2, h1Bits, 1, h2Bits, 1, maskedRs, 3, maskedRd, 3);
    }

    public void Emit(Format6OpCode opCode, LowRegister rd, int target) {
      var offset=(uint)(target-((CurrentAddress+4)&~2));
      CheckRange(target, 0, 1023, 3);
      var fluentComment="LDR ".MyConcat(rd, ",[PC, #0x", ((short)target).ToHex(), "]");
      var offsetBits=offset>>2;
      EmitHelper(rd, 6, fluentComment, 9, 5, rd.Index, 3, (int)offsetBits, 8);
    }

    public void Emit(Format7OpCode opCode, LowRegister rsd, LowRegister rb, LowRegister ro) {
      var fluentComment=opCode.ToHumanReadable().MyConcat(" ", rsd, ",[", rb, ",", ro, "]");
      EmitHelper(rsd, 7, fluentComment, 5, 4, (int)opCode, 2, 0, 1, ro.Index, 3, rb.Index, 3, rsd.Index, 3);
    }

    public void Emit(Format8OpCode opCode, LowRegister rsd, LowRegister rb, LowRegister ro) {
      var fluentComment=opCode.ToHumanReadable().MyConcat(" ", rsd, ",[", rb, ",", ro, "]");
      EmitHelper(rsd, 7, fluentComment, 5, 4, (int)opCode, 2, 1, 1, ro.Index, 3, rb.Index, 3, rsd.Index, 3);
    }

    public void Emit(Format9OpCode opCode, LowRegister rsd, LowRegister rb, byte offset) {
      CheckRange(offset, 0, 31);
      var multiplier=(opCode==Format9OpCode.LDR || opCode==Format9OpCode.STR) ? "*4" : "";
      var fluentComment=opCode.ToHumanReadable().MyConcat(" ", rsd, ",[", rb, ",#", offset+multiplier, "]");
      EmitHelper(rsd, 9, fluentComment, 3, 3, (int)opCode, 2, offset, 5, rb.Index, 3, rsd.Index, 3);
    }

    public void Emit(Format11OpCode opCode, LowRegister rd, uint unsigned10BitOffset) {
      CheckRange((int)unsigned10BitOffset, 0, 1023, 3);
      var fluentComment=opCode.ToHumanReadable().MyConcat(" ", rd, ",[SP,#", unsigned10BitOffset, "]");
      var offsetBits=(unsigned10BitOffset>>2)&0xff;
      EmitHelper(rd, 11, fluentComment, 9, 4, (int)opCode, 1, rd.Index, 3, (int)offsetBits, 8);
    }

    public void Emit(Format12OpCode opCode, LowRegister rd, int target) {
      var offset=target-((CurrentAddress+4)&~2);
      CheckRange(offset, 0, 1023, 3);
      var fluentComment=opCode.ToHumanReadable().MyConcat(" ", rd, ",#0x", ((short)target).ToHex());
      var offsetBits=(offset>>2)&0xff;
      EmitHelper(rd, 12, fluentComment, 10, 4, (int)opCode, 1, rd.Index, 3, offsetBits, 8);
    }

    public void EmitIfNecessary(Format13OpCode opCode, int signed9BitOffset) {
      if(signed9BitOffset==0) {
        return;
      }
      CheckRange(signed9BitOffset, -(1<<9)+1, (1<<9)-1, 3);
      var fluentComment="ADD SP,#"+signed9BitOffset;
      int signFlagBits;
      uint offsetBits;
      if(signed9BitOffset>0) {
        signFlagBits=0;
        offsetBits=(uint)signed9BitOffset;
      } else {
        signFlagBits=1;
        offsetBits=(uint)-signed9BitOffset;
      }
      offsetBits=offsetBits>>2;
      EmitHelper(null, 13, fluentComment, 11<<4, 8, signFlagBits, 1, (int)offsetBits, 7);
    }

    public void EmitIfNecessary(Format14OpCode opCode, bool includePcLr, byte registers) {
      var includePcLrText="";
      if(includePcLr) {
        includePcLrText=(opCode==Format14OpCode.POP ? ",PC" : ",LR");
      } else if(registers==0) {
        return;
      }
      var fluentComment=opCode.ToHumanReadable().MyConcat(" {regs=", ((int)registers).ToBinary(8), includePcLrText, "}");
      var loadStoreBit=opCode==Format14OpCode.POP ? 1 : 0;
      var pcLrBit=includePcLr ? 1 : 0;
      //HACK: we are explicitly excluding this call from the hacky "RegistersWritten" tracking
      //because our caller doesn't want it (our caller only wants registers that were touched
      //during its run, not saved and restored)
      EmitHelper(null, 14, fluentComment, 11, 4, loadStoreBit, 1, 2, 2, pcLrBit, 1, registers, 8);
    }

    public void Emit(Format16OpCode opCode, int target) {
      var offset=target-(CurrentAddress+4);
      CheckRange(offset, -(1<<8)+1, (1<<8)-1, 1);
      var fluentComment=opCode.ToHumanReadable()+" "+((short)target).ToHex();
      var offsetBits=(offset>>1)&0xff;
      EmitHelper(null, 16, fluentComment, 13, 4, (int)opCode, 4, offsetBits, 8);
    }

    public void Emit(Format18OpCode opCode, int target) {
      var offset=target-(CurrentAddress+4);
      CheckRange(offset, -(1<<12)+1, (1<<12)-1, 1);
      var fluentComment="B "+((short)target).ToHex();
      var offsetBits=(offset>>1)&0x7ff;
      EmitHelper(null, 18, fluentComment, 28, 5, offsetBits, 11);
    }

    private static void CheckRange(int value, int min, int max, int optionalMask=0) {
      if(value<min || value>max) {
        throw new Exception("value out of range");
      }
      if((value&optionalMask)!=0) {
        throw new Exception("value not aligned");
      }
    }

    public void Emit(Format19OpCode opCode, int target) {
      var offset=target-(CurrentAddress+4);
      var twoHalves=offset>>1;
      var topHalf=twoHalves>>11;
      var botHalf=twoHalves&0x7ff;
      var fluentComment="BL 0x"+target.ToHex();
      EmitHelper(null, 4, fluentComment+" (hi)", 15, 4, 0, 1, topHalf, 11);
      EmitHelper(null, 4, fluentComment+" (lo)", 15, 4, 1, 1, botHalf, 11);
    }

    public void EmitPlaceholder(string why) {
      var r8=new HighRegister(8);
      Emit(Format5OpCode.MOV, r8, r8, why);
    }

    private void EmitHelper(Register writtenRegister, int format, string fluentComment,
      int field0, int length0,
      int field1, int length1,
      int field2=0, int length2=0) {
      EmitHelper(writtenRegister, format, fluentComment, field0, length0, field1, length1, field2, length2, 0, 0, 0, 0, 0, 0);
    }

    /// <summary>
    /// A params array would be simpler here, but the IL space it takes to populate such an array
    /// depresses me
    /// </summary>
    private void EmitHelper(Register writtenRegister, int format, string fluentComment,
      int field0, int length0,
      int field1, int length1,
      int field2, int length2,
      int field3, int length3,
      int field4=0, int length4=0,
      int field5=0, int length5=0) {

      var ocb=new OpCodeBuilder();
      ocb.Add(field0, length0);
      ocb.Add(field1, length1);
      ocb.Add(field2, length2);
      ocb.Add(field3, length3);
      ocb.Add(field4, length4);
      ocb.Add(field5, length5);

      var assemblerComment=ocb.GetAssemblerComment().PaddedTo(25)+"(Format "+format+")";
      Emit((short)ocb.OpCode, fluentComment, assemblerComment);

      if(writtenRegister!=null) {
        RegistersWritten|=1<<writtenRegister.Index;
      }
    }

    private class OpCodeBuilder {
      private int opCode;
      public int OpCode { get { return opCode; } }

      private int offset=16; //fill in from left to right
      private readonly ArrayList binaryExpansion=new ArrayList();

      public void Add(int field, int length) {
        if(length==0) {
          return;
        }
        if(offset!=16) {
          binaryExpansion.Add("|");
        }
        offset-=length;
        var mask=(1<<length)-1;
        opCode|=(field&mask)<<offset;
        binaryExpansion.Add(field.ToBinary(length));
      }

      public string GetAssemblerComment() {
        if(offset!=0) {
          throw new Exception("Totals don't add up to 16");
        }
        return string.Concat((string[])binaryExpansion.ToArray(typeof(string)));
      }
    }

    public void EmitRegisterMoveIfDifferent(Register rd, Register rs) {
      if(rd.Index!=rs.Index) {
        var lowRd=rd as LowRegister;
        var lowRs=rs as LowRegister;
        if(lowRd!=null && lowRs!=null) {
          Emit(Format2OpCode.ADD, lowRd, lowRs, 0);
        } else {
          Emit(Format5OpCode.MOV, rd, rs);
        }
      }
    }

    public void EmitLoadAddress(Format12OpCode opCode, LowRegister targetRegister, int target, bool thumbAdjustment) {
      var offset=target-((CurrentAddress+4)&~2);
      if(offset>=0) {
        Emit(opCode, targetRegister, target);
        if(thumbAdjustment) {
          Emit(Format3OpCode.ADD, targetRegister, 1);
        }
      } else {
        //While the Format12 instruction will clear the 1 bit of the PC, this code does not
        var remainder=-(target-(CurrentAddress+4));
        if(thumbAdjustment) {
          --remainder;
        }
        Emit(Format5OpCode.MOV, targetRegister, Register.PC);
        while(remainder!=0) {
          var amountToUse=Math.Min(remainder, 255);
          Emit(Format3OpCode.SUB, targetRegister, (byte)remainder);
          remainder-=amountToUse;
        }
      }
    }
  }
}
