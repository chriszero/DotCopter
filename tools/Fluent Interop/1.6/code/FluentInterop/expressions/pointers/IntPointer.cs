namespace FluentInterop.Expressions {
  public abstract class IntPointer : Pointer {
    public static IntPointer operator+(IntPointer lhs, IntExpression rhs) {
      return (lhs.AsInt()+rhs.ShiftLeft(2)).AsIntPointer();
    }

    public IndirectIntReference this[IntExpression index] {
      get {
        return new IndirectIntReference(this, index);
      }
    }
  }
}
