using FluentInterop.BoolExpressions;
using FluentInterop.Expressions;
using FluentInterop.Representations;

namespace FluentInterop.CodeGeneration {
  /// <summary>
  /// Our various callbacks for our sort-of-functional programming
  /// </summary>
  public delegate void Action();
  public delegate void ActionOnCodeGenerator(CodeGenerator g);
  public delegate void ActionOnFuncBuilder(FuncBuilder f);
  public delegate void ActionOnIntExpr(IntExpression expression);
  public delegate void ActionOnIntVar(IntVariable intVariable);
  public delegate void ActionOnThreeLowRegisters(LowRegister target, LowRegister lhsReg, LowRegister rhsReg);
  public delegate void ActionOnTwoLowRegistersAndAByte(LowRegister target, LowRegister lhsReg, byte immediate);
  public delegate BoolExpression IntExprToBoolExpr(IntExpression intExpression);
  public delegate IReference RepresentationToIReference(Representation representation);
}
