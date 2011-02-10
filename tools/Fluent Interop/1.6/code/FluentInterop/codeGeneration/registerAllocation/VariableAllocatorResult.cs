using FluentInterop.Coding;

namespace FluentInterop.CodeGeneration.RegisterAllocation {
  public class VariableAllocatorResult {
    public readonly int UsedRegisterMask;
    public readonly int AllocatedVariableOffset;
    public readonly ReadOnlyDictionary VariableNameToRepresentation;
    public readonly bool RunAgain;

    public VariableAllocatorResult(int usedRegisterMask, int allocatedVariableOffset, ReadOnlyDictionary variableNameToRepresentation, bool runAgain) {
      this.UsedRegisterMask=usedRegisterMask;
      AllocatedVariableOffset=allocatedVariableOffset;
      VariableNameToRepresentation=variableNameToRepresentation;
      RunAgain=runAgain;
    }
  }
}
