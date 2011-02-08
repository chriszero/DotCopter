namespace DotCopter.ControlAlgorithms.Implementations.PID
{
    public class ProportionalIntegralDerivativeSettings
    {
        private readonly float _derivativeGain;
        private readonly float _integralGain;
        private readonly float _proportionalGain;
        private readonly float _windupGuard;

        public ProportionalIntegralDerivativeSettings(float proportionalGain, float integralGain, float derivativeGain, float windupGuard)
        {
            _derivativeGain = derivativeGain;
            _integralGain = integralGain;
            _proportionalGain = proportionalGain;
            _windupGuard = windupGuard;
        }

        public float WindupGuard
        {
            get { return _windupGuard; }
        }

        public float ProportionalGain
        {
            get { return _proportionalGain; }
        }

        public float IntegralGain
        {
            get { return _integralGain; }
        }

        public float DerivativeGain
        {
            get { return _derivativeGain; }
        }
    }
}
