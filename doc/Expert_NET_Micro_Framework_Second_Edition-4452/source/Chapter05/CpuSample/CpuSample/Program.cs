using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace CpuSample
{
    public class Program
    {
        public static void Main()
        {
            Cpu.GlitchFilterTime = new TimeSpan(0, 0, 0, 0, 100); //100 ms

            float systemClock = Cpu.SystemClock / 1000000.0f;
            Debug.Print("System Clock: " + systemClock.ToString("F6") + " MHz");
            float slowClock = Cpu.SlowClock / 1000000.0f;
            Debug.Print("Slow Clock: " + slowClock.ToString("F6") + " MHz");
            float glitchFilterTimeMs = Cpu.GlitchFilterTime.Ticks / (float)TimeSpan.TicksPerMillisecond;
            Debug.Print("Glitch Filter Time: " + glitchFilterTimeMs.ToString("F1") + " ms");
        }
    }
}
