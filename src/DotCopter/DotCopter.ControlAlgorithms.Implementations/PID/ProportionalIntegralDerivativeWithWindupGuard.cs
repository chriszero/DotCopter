using DotCopter.ControlAlgorithms.PID;

namespace DotCopter.ControlAlgorithms.Implementations.PID
{
    public class ProportionalIntegralDerivativeWithWindupGuard : IProportionalIntegralDerivative
    {
        private readonly ProportionalIntegralDerivativeSettings _settings;

        private float _lastProcessVariable;
        private float _integralError;

        public ProportionalIntegralDerivativeWithWindupGuard(ProportionalIntegralDerivativeSettings settings)
        {
            _settings = settings;
        }

        public void Update(float setpoint, float actualPosition, float deltaTimeGain)
        {
            float proportionalError = setpoint - actualPosition;

            _integralError += proportionalError * deltaTimeGain;
            if (_integralError > _settings.WindupGuard) _integralError = _settings.WindupGuard;
            if (_integralError < -1 * _settings.WindupGuard) _integralError = -1 * _settings.WindupGuard;

            float derivativeError = actualPosition - _lastProcessVariable;

            _lastProcessVariable = actualPosition;

            Output = proportionalError * _settings.ProportionalGain + _integralError * _settings.IntegralGain + derivativeError * _settings.DerivativeGain;
        }

        public float Output{get; private set; }
        
    }
}
