using FluentInterop.CodeGeneration;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public sealed class UnaryMinus : IntExpression {
    public static IntExpression Create(IntExpression expr) {
      int value;
      if(expr.TryGetConstant(out value)) {
        return new IntConstant(-value);
      }
      var alreadyNegatedExpression=expr as UnaryMinus;
      return ReferenceEquals(alreadyNegatedExpression, null) 
        ? new UnaryMinus(expr) 
        : alreadyNegatedExpression.expr;
    }

    private readonly IntExpression expr;

    public UnaryMinus(IntExpression expr) {
      this.expr=expr;
    }

    public override bool IsNegated {
      get { return true; }
    }

    protected override IReadable EvaluateToHelper(IReference storage) {
      var emitter=CodeGenerator.Emitter;

      var exprResult=expr.EvaluateTo(storage);
      var storageReg=storage.ProposeRegisterOrScratch0();
      var exprReg=exprResult.ToRegister(storageReg);
      emitter.Emit(OpCodes.Format4OpCode.NEG, storageReg, exprReg);
      storage.FromRegister(storageReg);
      return storage;
    }

    public override bool HasConflictWith(IReference storage) {
      return expr.HasConflictWith(storage);
    }
  }
}
