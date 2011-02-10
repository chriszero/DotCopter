namespace FluentInterop.Expressions.Pio {
  public abstract class PIOReference : Expression {
    public IntExpression PER {
      set { SetHelper(0, value); }
    }
    public IntExpression OER {
      set { SetHelper(4, value); }
    }
    public IntExpression SODR {
      set { SetHelper(12, value); }
    }
    public IntExpression CODR {
      set { SetHelper(13, value); }
    }
    private void SetHelper(int offset, IntExpression value) {
      this.AsIntPointer()[offset].Value=value;
    }
  }
}
