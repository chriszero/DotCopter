using DotCopter.Commons.Utilities;

namespace DotCopter.Hardware.Radio
{
    public class RadioSettings
    {
        public RadioSettings(Scale throttleScale, Scale axesScale, float radioSensitivityFactor)
        {
            ThrottleScale = throttleScale;
            AxesScale = axesScale;
            RadioSensitivityFactor = radioSensitivityFactor;
        }

        public float RadioSensitivityFactor { get; private set; }

        public Scale AxesScale { get; private set; }

        public Scale ThrottleScale { get; private set; }
    }
}
