using DotCopter.ControlAlgorithms.PID;

namespace DotCopter.ControlAlgorithms.Implementations.PID
{
    /// <summary>
    ///PID ideal, parallel  http://en.wikipedia.org/wiki/PID_controller#Discrete_implementation
    /// </summary>

    public class PID : ControlAlgorithms.PID.PID
    {
        private float _integral;
        private float _previousError;


        public PID(PIDSettings settings) : base(settings)
        {
        }

        public override void Update(float setpoint, float actualPosition, float deltaTimeGain)
        {
            float error = setpoint - actualPosition;
            _integral = _integral + (error*deltaTimeGain);
            float derivative = (error - _previousError);
            _previousError = error;
            Output = (Settings.ProportionalGain * error) + (Settings.IntegralGain * _integral) + (Settings.DerivativeGain * derivative);
        }
    }
}
