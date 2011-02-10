using DotCopter.Avionics;
using DotCopter.Commons.Utilities;
using DotCopter.Hardware.Implementations.Bus;
using DotCopter.Hardware.Radio;
using Microsoft.SPOT.Hardware;

namespace DotCopter.Hardware.Implementations.Radio
{
    public class DefaultRadio : Hardware.Radio.Radio
    {
        private readonly RadioSettings _settings;
        private readonly byte[] _buffer = new byte[10];
        private readonly I2CBus _twiBus;
        private readonly I2CDevice.Configuration _i2cConfiguration = new I2CDevice.Configuration(0x42, 400);

        public DefaultRadio(I2CBus twiBus, RadioSettings settings) : 
            base(0,new AircraftPrincipalAxes { Pitch = 0, Roll = 0, Yaw = 0 },false)
        {
            _twiBus = twiBus;
            _settings = settings;
        }

        public override void Update()
        {
            _twiBus.Read(_i2cConfiguration, _buffer, 100);
            Throttle = _settings.ThrottleScale.Calculate(BitConverter.ToShort(_buffer,0));
            Axes.Pitch = _settings.AxesScale.Calculate(BitConverter.ToShort(_buffer, 2)) * _settings.RadioSensitivityFactor;
            Axes.Roll = _settings.AxesScale.Calculate(BitConverter.ToShort(_buffer, 4)) * _settings.RadioSensitivityFactor;
            Axes.Yaw = _settings.AxesScale.Calculate(BitConverter.ToShort(_buffer, 6)) * _settings.RadioSensitivityFactor;
            Gear = BitConverter.ToShort(_buffer, 8) > 1500;
        }
        
    }

    
}
