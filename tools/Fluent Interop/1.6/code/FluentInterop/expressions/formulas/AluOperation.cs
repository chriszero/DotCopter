using FluentInterop.CodeGeneration;
using FluentInterop.OpCodes;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public sealed class AluOperation : BinaryIntExpression {
    private readonly Format4OpCode opCode;

    public AluOperation(IntExpression lhs, IntExpression rhs, Format4OpCode opCode) : base(lhs, rhs) {
      this.opCode=opCode;
    }

    protected override IReadable EvaluateToHelper(IReference storage) {
      var f=FuncBuilder.Instance;
      using(f.OpenScope(opCode.ToHumanReadable())) {
        var emitter=CodeGenerator.Emitter;

        var sm=new StorageManager(storage);
        var lhsResult=lhs.EvaluateTo(sm.ForLhs(rhs));
        var rhsResult=rhs.EvaluateTo(sm.ForRhs(lhsResult));

        var storageReg=f.Scratch0;
        var lhsReg=lhsResult.ToRegister(f.Scratch0);
        var rhsReg=rhsResult.ToRegister(f.Scratch1);
        emitter.EmitRegisterMoveIfDifferent(storageReg, lhsReg);
        emitter.Emit(opCode, storageReg, rhsReg);
        storage.FromRegister(storageReg);
        return storage;
      }
    }
  }
}
