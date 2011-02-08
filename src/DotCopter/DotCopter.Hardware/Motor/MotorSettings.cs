using DotCopter.Commons.Utilities;

namespace DotCopter.Hardware.Motor
{
    public class MotorSettings
    {
        public MotorSettings(Scale motorScale, float safeOutput, float minimumOutput, float maximumOutput )
        {
            MotorScale = motorScale;
            SafeOutput = safeOutput;
            MinimumOutput = minimumOutput;
            MaximumOutput = maximumOutput;
        }

        public Scale MotorScale { get; private set; }
        public float SafeOutput { get; private set; }
        public float MinimumOutput { get; private set; }
        public float MaximumOutput { get; private set; }
    }
}
