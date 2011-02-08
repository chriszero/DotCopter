using DotCopter.ControlAlgorithms.PID;

namespace DotCopter.ControlAlgorithms.Implementations.PID
{
    /// <summary>
    ///PID ideal, parallel  http://en.wikipedia.org/wiki/PID_controller#Discrete_implementation
    /// </summary>

    public class ProportionalIntegralDerivative : IProportionalIntegralDerivative
    {
        private readonly ProportionalIntegralDerivativeSettings _settings;
        private float _integral;
        private float _previousError;

        public ProportionalIntegralDerivative(ProportionalIntegralDerivativeSettings settings)
        {
            _settings = settings;
        }

        public void Update(float setpoint, float actualPosition, float deltaTimeGain)
        {
            float error = setpoint - actualPosition;
            _integral = _integral + (error*deltaTimeGain);
            float derivative = (error - _previousError);
            _previousError = error;
            Output = (_settings.ProportionalGain * error) + (_settings.IntegralGain * _integral) + (_settings.DerivativeGain * derivative);
        }

        public float Output{get; private set; }
        
    }
}
