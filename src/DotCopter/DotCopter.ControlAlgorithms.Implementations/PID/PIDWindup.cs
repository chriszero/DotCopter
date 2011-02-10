using DotCopter.Commons.Utilities;
using DotCopter.ControlAlgorithms.PID;

namespace DotCopter.ControlAlgorithms.Implementations.PID
{
    public class PIDWindup : ControlAlgorithms.PID.PID
    {
        private float _lastProcessVariable;
        private float _integralError;

        public PIDWindup(PIDSettings settings) : base(settings)
        {
        }

        public override void Update(float setpoint, float actualPosition, float deltaTimeGain)
        {
            float proportionalError = setpoint - actualPosition;

            _integralError += proportionalError * deltaTimeGain;
            _integralError = Logic.Constrain(_integralError, -1 * Settings.WindupLimit, Settings.WindupLimit);

            float derivativeError = actualPosition - _lastProcessVariable;

            _lastProcessVariable = actualPosition;

            Output = proportionalError * Settings.ProportionalGain + _integralError * Settings.IntegralGain + derivativeError * Settings.DerivativeGain;
        }
        
    }
}
