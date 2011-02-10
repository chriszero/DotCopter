namespace FluentInterop.CodeGeneration.RegisterAllocation {
  public sealed class AllocatorStats {
    public readonly int AllFreeRegisters;
    public readonly int LastUsedStackIndex;

    public AllocatorStats(int allFreeRegisters, int lastUsedStackIndex) {
      this.AllFreeRegisters=allFreeRegisters;
      this.LastUsedStackIndex=lastUsedStackIndex;
    }
  }
}
