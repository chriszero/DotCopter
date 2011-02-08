using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace AlarmClockSample
{
    public class Program
    {
        public static void Main()
        {
            DateTime localSystemTime = new DateTime(2009, 9, 19, 13, 10, 0, 0);
            Utility.SetLocalTime(localSystemTime);
            Debug.Print("It is now " + DateTime.Now);
            //alarm in 5 secs
            DateTime alarmTime = new DateTime(2009, 9, 19, 13, 10, 5, 0);
            ExtendedTimer alarmTimer = new ExtendedTimer(new TimerCallback(OnTimer),
                                                         null,
                                                         alarmTime,
                                                         TimeSpan.Zero); //one shot (or new TimeSpan(-1))
            Thread.Sleep(Timeout.Infinite); //low power consumption here
            Debug.Print("End of program. Should be never reached.");
        }

        private static void OnTimer(object target)
        {
            Debug.Print("Ring, Ring, it is now " + DateTime.Now);
        }
    }
}
