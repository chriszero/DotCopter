using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace BatterySample
{
    public class Program
    {
        public static void Main()
        {
            PrintBatteryInfo();
            PrintChargerModel();
            while (true)
            {
                if (Battery.WaitForEvent(Timeout.Infinite))
                    PrintBatteryInfo();
            }
        }

        private static void PrintBatteryInfo()
        {
            Debug.Print("*** Battery Info ***");
            Debug.Print("State of Charge: " + Battery.StateOfCharge() + "%");
            Debug.Print("Is fully charged: " + (Battery.IsFullyCharged() ? "Yes" : "No"));
            float voltageVolt = Battery.ReadVoltage() / 1000.0f;
            Debug.Print("Voltage: " + voltageVolt.ToString("F3") + " Volt");
            float degreesCelsius = Battery.ReadTemperature() / 10.0f;
            Debug.Print("Temperature: " + degreesCelsius.ToString("F1") + "° Celsius");
            Debug.Print("On Charger: " + (Battery.OnCharger() ? "Yes" : "No"));
        }

        private static void PrintChargerModel()
        {
            Battery.ChargerModel chargerModel = Battery.GetChargerModel();

            Debug.Print("*** Charger Model ***");
            Debug.Print("Charge Min: " + chargerModel.Charge_Min + "%");
            Debug.Print("Charge Low: " + chargerModel.Charge_Low + "%");
            Debug.Print("Charge Medium: " + chargerModel.Charge_Medium + "%");
            Debug.Print("Charge Full Min: " + chargerModel.Charge_FullMin + "%");
            Debug.Print("Charge Full: " + chargerModel.Charge_Full + "%");
            Debug.Print("Charge Hysteresis: " + chargerModel.Charge_Hysteresis + " ms");
            float timeoutChargingMinutes = chargerModel.Timeout_Charging.Ticks / (float)TimeSpan.TicksPerMinute;
            Debug.Print("Timeout Charging: " + timeoutChargingMinutes.ToString("F0") + " min");
            float timeoutChargedMinutes = chargerModel.Timeout_Charged.Ticks / (float)TimeSpan.TicksPerMinute;
            Debug.Print("Timeout Charged: " + timeoutChargedMinutes.ToString("F0") + " min");
            float timeoutChargerSec = chargerModel.Timeout_Charger.Ticks / (float)TimeSpan.TicksPerSecond;
            Debug.Print("Timeout Charger: " + chargerModel.Timeout_Charger.Seconds + " sec");
            float timeoutBacklightSec = chargerModel.Timeout_Backlight.Ticks / (float)TimeSpan.TicksPerSecond;
            Debug.Print("Timeout Backlight: " + timeoutBacklightSec.ToString("F0") + " sec");
        }
    }
}
