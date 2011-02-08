using DotCopter.Avionics;

namespace DotCopter.Hardware.Motor
{
    public interface IMotors
    {
        void SetSafe();
        void Update(float throttle, AircraftPrincipalAxes output);
    }
}
