namespace DotCopter.ControlAlgorithms.PID
{
    public struct PIDSettings
    {
        public float ProportionalGain;
        public float IntegralGain;
        public float DerivativeGain;
        public float WindupLimit;
    }
}
