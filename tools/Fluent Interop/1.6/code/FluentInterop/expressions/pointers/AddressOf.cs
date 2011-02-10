using System;
using FluentInterop.CodeGeneration;
using FluentInterop.CodeGeneration.Entities;
using FluentInterop.OpCodes;
using FluentInterop.Representations;

namespace FluentInterop.Expressions.Pointers {
  public class AddressOf : Pointer {
    public static AddressOf FromStaticVariable(IReference variable) {
      var vi=CodeGenerator.Instance.GetStaticVariableInfo(variable, false);
      if(vi==null) {
        throw new Exception("Can't take the address of a non-static variable");
      }
      var rep=vi.Representation;
      return new AddressOf(rep.StorageLabel, false);
    }

    private readonly Label label;
    private readonly bool wantThumbAdjustment;

    public AddressOf(Label label, bool wantThumbAdjustment) {
      this.label=label;
      this.wantThumbAdjustment=wantThumbAdjustment;
    }

    protected override IReadable EvaluateToHelper(IReference storage) {
      var emitter=CodeGenerator.Emitter;

      var myAddress=label.GetLabelAddressBestEffort();

      var targetRegister=storage.ProposeRegisterOrScratch0();
      emitter.EmitLoadAddress(Format12OpCode.LDADDR_PC, targetRegister, myAddress, wantThumbAdjustment);
      storage.FromRegister(targetRegister);
      return storage;
    }
  }
}
