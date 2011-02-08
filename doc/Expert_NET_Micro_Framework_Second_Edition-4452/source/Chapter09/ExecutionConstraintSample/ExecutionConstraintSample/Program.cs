using System;
using System.Threading;
using Microsoft.SPOT;

namespace ExecutionConstraintSample
{
    public class Program
    {
        public static void Main()
        {
            const int timeout = 100; // 100 ms = maximum accepted duration of operation

            ExecutionConstraint.Install(timeout, 0); //install to check constraint
            //do something which must take less than timeout
            Thread.Sleep(50); //operation is executed in less time
            //end of operation
            ExecutionConstraint.Install(-1, 0); //uninstall
            Debug.Print("First operation successfully executed.");

            ExecutionConstraint.Install(timeout, 0); //install to check constraint
            //do something which must take less than timeout
            Thread.Sleep(150); //operation takes longer as forced, so an exception will be thrown after timeout ms
            //end of operation
            ExecutionConstraint.Install(-1, 0); //uninstall
            Debug.Print("Second operation successfully executed.");
        }
    }
}
