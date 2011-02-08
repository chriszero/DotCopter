using DotCopter.Avionics;

namespace DotCopter.Hardware.Radio
{
    public interface IRadio
    {
        float Throttle { get; }
        bool Gear { get; }
        AircraftPrincipalAxes Axes { get; }
        void Update();
        //event GearDelegate HaveChangedGearEvent;
    }
}
