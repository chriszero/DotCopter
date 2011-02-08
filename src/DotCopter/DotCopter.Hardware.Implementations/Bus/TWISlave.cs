using Microsoft.SPOT.Hardware;

namespace DotCopter.Hardware.Implementations.Bus
{
    public class TWISlave
    {
        private readonly I2CDevice.Configuration _slaveConfig;
        private readonly TWIBus _twiBus;

        protected TWISlave(ushort address, int clockRateKhz, TWIBus twiBus)
        {
            _slaveConfig = new I2CDevice.Configuration(address, clockRateKhz);
            _twiBus = twiBus;
        }

        protected void Read(ref byte[] buffer, int timeout)
        {
            _twiBus.Read(_slaveConfig, buffer, timeout);
        }
        protected void Write(byte[] buffer, int timeout)
        {
            _twiBus.Write(_slaveConfig, buffer, timeout);
        }
    }

}
