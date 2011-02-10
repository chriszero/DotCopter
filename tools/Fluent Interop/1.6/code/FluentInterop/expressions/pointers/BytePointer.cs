namespace FluentInterop.Expressions {
  public abstract class BytePointer : Pointer {
    public IndirectByteReference this[IntExpression index] {
      get {
        return new IndirectByteReference(this, index);
      }
    }
  }
}
