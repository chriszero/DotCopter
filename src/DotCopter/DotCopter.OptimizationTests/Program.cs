using System;
using Microsoft.SPOT;

namespace DotCopter.OptimizationTests
{
    public class Program
    {
        public static void Main()
        {
            double ret;//held here to ensure cals are actually done

            //in Hz
            const long radioFrequency = 250;
            const long motorFrequency = 500;
            const long sensorFrequency = 250;
            const long statusFrequency = 1;

            long lastRadioTime= DateTime.Now.Ticks;
            long lastMotorTime = DateTime.Now.Ticks;
            long lastSensorTime = DateTime.Now.Ticks;
            long lastInfoTime = DateTime.Now.Ticks;
            
            
            const long loopUnit = 10000000;
            GCTests gctest = new GCTests();

            long loopCounter = 1;
            long sensorCounter = 1;
            long motorCounter=1;
            long radioCounter = 1;

            Debug.EnableGCMessages(true);
            DateTime start = DateTime.Now;
            while (true)
            {
                
                long currentTime = DateTime.Now.Ticks;
                if ((currentTime > (lastRadioTime + (loopUnit / radioFrequency))))
                {
                    ret = gctest.RunMathOperationWithGc();
                    lastRadioTime = DateTime.Now.Ticks;
                    radioCounter++;
                }

                if ((currentTime > (lastMotorTime + (loopUnit / motorFrequency))))
                {
                    ret = gctest.RunMathOperationWithGc();
                    lastMotorTime = DateTime.Now.Ticks;
                    motorCounter++;
                }


                if ((currentTime > (lastSensorTime + (loopUnit / sensorFrequency))))
                {
                    ret = gctest.RunMathOperationWithGc();
                    lastSensorTime = DateTime.Now.Ticks;
                    sensorCounter++;
                }

                loopCounter++;
                

                if ((currentTime > (lastInfoTime + (loopUnit / statusFrequency))))
                {
                    DateTime end = DateTime.Now;
                    TimeSpan span = end - start;

                    long loopFrequency = (span.Ticks * loopCounter) / loopUnit;
                    long sensorActualFrequency = (span.Ticks * sensorCounter) / loopUnit;
                    long motorActualFrequency = (span.Ticks * motorCounter) / loopUnit;
                    long radioActualFrequency = (span.Ticks * radioCounter) / loopUnit;

                    Debug.Print("o"+loopFrequency);
                    Debug.Print("s" + sensorActualFrequency);
                    Debug.Print("m" + motorActualFrequency);
                    Debug.Print("r" + radioActualFrequency);

                    
                    loopCounter = 0;
                    sensorCounter = 0;
                    motorCounter = 0;
                    radioCounter = 0;

                    start = DateTime.Now;
                    lastInfoTime = DateTime.Now.Ticks;
                }
            }
        }
    }
}
