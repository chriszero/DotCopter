using Microsoft.SPOT.Hardware;

namespace HardwareProviderSample
{
    internal sealed class MyHardwareProvider : HardwareProvider
    {
        public override void GetSerialPins(string comPort,
                                          out Cpu.Pin rxPin,
                                          out Cpu.Pin txPin,
                                          out Cpu.Pin ctsPin,
                                          out Cpu.Pin rtsPin)
        {
            switch (comPort)
            {
                case "COM1":
                    rxPin = Cpu.Pin.GPIO_Pin0;
                    txPin = Cpu.Pin.GPIO_Pin1;
                    ctsPin = Cpu.Pin.GPIO_Pin2;
                    rtsPin = Cpu.Pin.GPIO_Pin3;
                    break;
                case "Com2":
                    rxPin = Cpu.Pin.GPIO_Pin4;
                    txPin = Cpu.Pin.GPIO_Pin5;
                    ctsPin = Cpu.Pin.GPIO_Pin6;
                    rtsPin = Cpu.Pin.GPIO_Pin7;
                    break;
                default:
                    rxPin = Cpu.Pin.GPIO_NONE;
                    txPin = Cpu.Pin.GPIO_NONE;
                    ctsPin = Cpu.Pin.GPIO_NONE;
                    rtsPin = Cpu.Pin.GPIO_NONE;
                    break;
            }
        }
    }
}
