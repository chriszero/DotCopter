using DotCopter.Avionics;

namespace DotCopter.Hardware.Radio
{
    public abstract class Radio
    {
        public float Throttle;
        public AircraftPrincipalAxes Axes;
        public bool Gear;

        public Radio(float throttle, AircraftPrincipalAxes axes, bool gear)
        {
            Throttle = throttle;
            Axes = axes;
            Gear = gear;
        }
        public abstract void Update();
    }
}
