using DotCopter.Avionics;

namespace DotCopter.ControlAlgorithms.Mixing
{
    public abstract class MotorMixer
    {
        public abstract void SetSafe();
        public abstract void Update(float throttle, AircraftPrincipalAxes output);
    }
}