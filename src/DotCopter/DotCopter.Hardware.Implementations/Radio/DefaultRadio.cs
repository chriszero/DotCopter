using DotCopter.Avionics;
using DotCopter.Hardware.Implementations.Bus;
using DotCopter.Hardware.Radio;
using Microsoft.SPOT.Hardware;

namespace DotCopter.Hardware.Implementations.Radio
{
    public class DefaultRadio: TWISlave, IRadio
    {
        /*
        public event GearDelegate HaveChangedGearEvent;
        private void InvokeHaveChangedGearEvent()
        {
            GearDelegate handler = HaveChangedGearEvent;
            if (handler != null) handler(Gear);
        }
        */

        private readonly RadioSettings _settings;
        private byte[] _buffer = new byte[10];

        public DefaultRadio(TWIBus twiBus, RadioSettings settings): base(0x42, 400, twiBus)
        {
            _settings = settings;
            Axes = new AircraftPrincipalAxes(0, 0, 0);
        }

        public void Update()
        {
            Read(ref _buffer, 100);
            Throttle = _settings.ThrottleScale.Calculate(Utility.ExtractValueFromArray(_buffer, 0, 2));
            float roll = _settings.AxesScale.Calculate(Utility.ExtractValueFromArray(_buffer, 2, 2)) * _settings.RadioSensitivityFactor;
            float pitch = _settings.AxesScale.Calculate(Utility.ExtractValueFromArray(_buffer, 4, 2)) * _settings.RadioSensitivityFactor;
            float yaw = _settings.AxesScale.Calculate(Utility.ExtractValueFromArray(_buffer, 6, 2)) * _settings.RadioSensitivityFactor;
            Gear = Utility.ExtractValueFromArray(_buffer, 8, 2) > 1500;
            /*
            if(gear!=Gear)
            {
                Gear = gear;
                InvokeHaveChangedGearEvent();
            }
            */
            Axes.Update(pitch, roll, yaw);
        }

        public float Throttle { get; private set; }
        public bool Gear { get; private set; }
        public AircraftPrincipalAxes Axes { get; private set; }


    }

    
}
