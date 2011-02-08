using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace SystemInfoSample
{
    public class Program
    {
        public static void Main()
        {
            Debug.Print("Version: " + SystemInfo.Version);
            Debug.Print("Original Equipment Manufacturer (OEM): " + SystemInfo.OEMString);
            Debug.Print("System Identification:");
            Debug.Print(" Model: " + SystemInfo.SystemID.Model);
            Debug.Print(" Original Equipment Manufacturer (OEM): " + SystemInfo.SystemID.OEM);
            Debug.Print(" Stock Keeping Unit (SKU): " + SystemInfo.SystemID.SKU);
        }
    }
}
