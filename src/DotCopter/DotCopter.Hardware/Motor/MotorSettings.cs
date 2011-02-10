using DotCopter.Commons.Utilities;

namespace DotCopter.Hardware.Motor
{
    public class MotorSettings
    {
        public readonly Scale MotorScale;
        public readonly float SafeOutput;
        public readonly float MinimumOutput;
        public readonly float MaximumOutput;

        public MotorSettings(Scale motorScale, float safeOutput, float minimumOutput, float maximumOutput )
        {
            MotorScale = motorScale;
            SafeOutput = safeOutput;
            MinimumOutput = minimumOutput;
            MaximumOutput = maximumOutput;
        }

    }
}
