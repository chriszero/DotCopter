using System;
using Microsoft.SPOT;

namespace ExtendedWeakReferenceSample
{
    public class Program
    {
        //the weak reference, must be a static reference to work properly
        private static ExtendedWeakReference bootInfoExtendedWeakReference;

        //this the selector class to identify the data
        private static class MyUniqueSelectorType { }

        public static void Main()
        {
            //try to recover the data or create the weak reference
            bootInfoExtendedWeakReference =
                ExtendedWeakReference.RecoverOrCreate(
               typeof(MyUniqueSelectorType), //unique type to prevent other code from accessing our data
               0,                            //id that refers to the data we care about
               ExtendedWeakReference.c_SurvivePowerdown | ExtendedWeakReference.c_SurviveBoot);

            //set how important the data is
            bootInfoExtendedWeakReference.Priority = (int)ExtendedWeakReference.PriorityLevel.Important;

            //try to get the persisted data
            //if we get the data, then putting it in the field myBootInfo creates a 
            //strong reference to it, preventing the GC from collecting it
            MyBootInfo myBootInfo = (MyBootInfo)bootInfoExtendedWeakReference.Target;

            if (myBootInfo == null)
            {
                //the data could not be obtained
                Debug.Print("This is the first time the device booted or the data was lost.");
                myBootInfo = new MyBootInfo(1); //first initialization
            }
            else
            {
                //the data could be obtained, display info
                Debug.Print("Number of boots = " + myBootInfo.BootCount);
                myBootInfo = new MyBootInfo(myBootInfo.BootCount + 1); //increment number of boots
            }

            //setting the Target property causes to write the data to flash memory
            bootInfoExtendedWeakReference.Target = myBootInfo;

            //give the system time to save it to flash
            System.Threading.Thread.Sleep(2000);
        }
    }
}
