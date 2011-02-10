namespace DotCopter.ControlAlgorithms.PID
{
    public abstract class PID
    {
        public PIDSettings Settings;
        public float Output;
        public abstract void Update(float setPoint, float actualPosition, float deltaTimeGain);

        public PID(PIDSettings settings)
        {
            Settings = settings;
        }
    }
}
