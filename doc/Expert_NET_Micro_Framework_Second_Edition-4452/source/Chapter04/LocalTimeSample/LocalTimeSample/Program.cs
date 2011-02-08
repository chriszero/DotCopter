using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace LocalTimeSample
{
    public class Program
    {
        public static void Main()
        {
            Debug.Print("Local Time: " + DateTime.Now);
            DateTime newLocalTime = new DateTime(2009, 1, 2, 3, 4, 5);
            Utility.SetLocalTime(newLocalTime);
            Debug.Print("New Local Time: " + DateTime.Now);
            Debug.Print("Time Zone: " + TimeZone.CurrentTimeZone.StandardName);
            Debug.Print("Local Time: " + DateTime.Now);
            ExtendedTimeZone.SetTimeZone(TimeZoneId.Berlin);
            Debug.Print("New Time Zone: " + TimeZone.CurrentTimeZone.StandardName);
            Debug.Print("Local Time: " + DateTime.Now);
        }
    }
}
