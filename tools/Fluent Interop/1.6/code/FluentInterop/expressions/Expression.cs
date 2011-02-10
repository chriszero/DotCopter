using FluentInterop.CodeGeneration;
using FluentInterop.Representations;

namespace FluentInterop.Expressions {
  public abstract class Expression {
    public IReadable EvaluateTo(IReference storage) {
      var result=EvaluateToHelper(storage);
      var reference=result as IReference;
      if(reference!=null) {
        FuncBuilder.Instance.NoteRead(reference);
      }
      return result;
    }

    protected abstract IReadable EvaluateToHelper(IReference storage);

    public virtual bool HasConflictWith(IReference storage) {
      return false;
    }

    /// <summary>
    /// At the moment none of my expressions have side effects, but someday they will
    /// (e.g. function calls or maybe autoincrement)
    /// </summary>
    public void EvaluateForItsSideEffects() {
    }

    public bool IsConstant() {
      int dummy;
      return TryGetConstant(out dummy);
    }

    /// <summary>
    /// This is overridden by the IntConstant class
    /// </summary>
    public virtual bool TryGetConstant(out int value) {
      value=0;
      return false;
    }

    public IntExpression AsInt() {
      return new IntCast(this);
    }
    public IntPointer AsIntPointer() {
      return new IntPointerCast(this);
    }
    public FuncPointer AsFuncPointer() {
      return new FuncPointerCast(this);
    }
  }
}
