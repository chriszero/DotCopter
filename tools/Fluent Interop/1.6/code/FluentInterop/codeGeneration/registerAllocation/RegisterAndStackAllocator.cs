using System;
using FluentInterop.CodeGeneration.Entities;
using FluentInterop.Representations;

namespace FluentInterop.CodeGeneration.RegisterAllocation {
  public sealed class RegisterAndStackAllocator {
    private int numberOfRegistersLeft;
    private int numberOfStackWordsLeft;

    private int currentFreeRegisters;
    private int currentLastUsedStackIndex;

    public RegisterAndStackAllocator(AllocatorStats stats, int totalNumberOfVariables, int numberOfThoseWhichAreRegisters) {
      this.numberOfRegistersLeft=numberOfThoseWhichAreRegisters;
      this.numberOfStackWordsLeft=totalNumberOfVariables-numberOfThoseWhichAreRegisters;
      this.currentFreeRegisters=stats.AllFreeRegisters;
      this.currentLastUsedStackIndex=stats.LastUsedStackIndex;
    }

    public AllocatorStats MakeNewStats() {
      return new AllocatorStats(currentFreeRegisters, currentLastUsedStackIndex);
    }

    public Representation AllocateRepresentation(TerminalName variableName) {
      return (Representation)AllocateRegisterIfPossible(null) ?? AllocateStackTemporary();
    }

    private LowRegister AllocateRegisterIfPossible(LowRegister register) {
      if(numberOfRegistersLeft!=0) {
        if(register!=null && TryAllocate(register.Index)) {
          return register;
        }
        for(var i=0; i<32; ++i) {
          if(TryAllocate(i)) {
            return new LowRegister(i);
          }
        }
      }
      return null;
    }

    private bool TryAllocate(int index) {
      var mask=(1<<index);
      if((currentFreeRegisters&mask)!=0) {
        currentFreeRegisters&=~mask;
        --numberOfRegistersLeft;
        return true;
      }
      return false;
    }

    private StackWord AllocateStackTemporary() {
      if(numberOfStackWordsLeft==0) {
        throw new Exception("ran out of stack?");
      }
      --numberOfStackWordsLeft;
      currentLastUsedStackIndex-=4;
      return new StackWordRelativeToEndOfVariables(currentLastUsedStackIndex);
    }
  }
}
