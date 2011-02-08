using System;

namespace MFProbingClientSample
{
    public class Program
    {
        public static void Main()
        {
            using (ProbingClient client = new ProbingClient())
            {
                client.PrintProbeMatchInfo();
            }
        }
    }
}
