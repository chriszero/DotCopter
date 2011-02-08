namespace DotCopter.ControlAlgorithms.PID
{
    public interface IProportionalIntegralDerivative
    {
        void Update(float setPoint, float actualPosition, float deltaTimeGain);
        float Output { get; }
    }
}
