using FluentInterop.CodeGeneration.Util;
using FluentInterop.Expressions;
using FluentInterop.OpCodes;
using FluentInterop.Representations;

namespace FluentInterop.CodeGeneration {
  partial class FuncBuilder {
    public void Invoke(FuncPointer fp, IReference optionalResult, params Expression[] arguments) {
      var callerCaresAboutResult=!ReferenceEquals(optionalResult, null) && !CanProveIsNeverRead(optionalResult);

      var g=CodeGenerator.Instance;
      var emitter=CodeGenerator.Emitter;

      using(this.OpenScope("invokeMethod")) {
        this.externalMethodWasInvoked=true;

        //use the caller's storage (if any) as a place to evaluate the func pointer
        var temp=this.Declare.Int("temp");
        var fpReadable=fp.EvaluateTo(optionalResult ?? temp);

        using(this.OpenScope("params")) {
          //SUPER HACK: in order to participate in the function calling convention, I need to
          //relocate any existing variable stored in R0-R3 to the stack
          int registerMaskToSpill;
          int registersToSpillStackConsumption;
          localVariableToInfo.MigrateR0R3ToStack(out registerMaskToSpill, out registersToSpillStackConsumption);

          //Spill the affected registers to the stack
          emitter.EmitIfNecessary(Format14OpCode.PUSH, false, (byte)registerMaskToSpill);
          StackPointer+=registersToSpillStackConsumption;

          //great!  Now make your parameters (which may further adjust the stack)
          //we only need parameters up to our highest unused argument
          var parameterLength=arguments.Length;
          while(parameterLength>0 && arguments[parameterLength-1]==null) {
            --parameterLength;
          }
          var stackPointerBeforePush=StackPointer;
          var parameters=new IReference[parameterLength];
          for(var i=parameterLength-1; i>=0; --i) {
            Representation representation;
            if(i<4) {
              representation=new LowRegister(i);
            } else {
              StackPointer+=-4;
              representation=new StackWordRelativeToZero(StackPointer);
            }
            var vi=new LocalVariableInfo(CreateTerminalName("param"+i), representation);
            var variable=new IntVariable();

            localVariableToInfo.Add(variable, vi);
            parameters[i]=variable;
          }

          var additionalStackToAllocate=StackPointer-stackPointerBeforePush;
          emitter.EmitIfNecessary(Format13OpCode.ADDSP, additionalStackToAllocate);

          //Now move your arguments to the function parameters
          for(var i=0; i<parameterLength; ++i) {
            var parameter=parameters[i];
            var argument=arguments[i];
            if(!ReferenceEquals(argument, null)) {
              Assignment.SpecialAssignAny(parameter, arguments[i]);
            }
          }

          //FIRE AWAY, BONGO
          var branchTargetReg=fpReadable.ToRegister(Scratch0);
          var targetLabel=g.LookupOrCreateBranchTo(branchTargetReg);
          var address=targetLabel.GetLabelAddressBestEffort();
          emitter.Emit(Format19OpCode.BL, address);

          //woowee!  we are back
          //R0 has a result... stash it in LR so that our pop logic can restore everything else
          if(callerCaresAboutResult) {
            emitter.EmitRegisterMoveIfDifferent(Register.LR, Register.R0);
          }

          //fix your stack
          emitter.EmitIfNecessary(Format13OpCode.ADDSP, -additionalStackToAllocate);
          StackPointer-=additionalStackToAllocate;

          //fix your registers
          emitter.EmitIfNecessary(Format14OpCode.POP, false, (byte)registerMaskToSpill);
          StackPointer-=registersToSpillStackConsumption;
        }
      }

      //if the caller cares about the result, provide it
      if(callerCaresAboutResult) {
        optionalResult.FromRegister(Register.LR);
      }
    }
  }
}
