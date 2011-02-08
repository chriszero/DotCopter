using System;

namespace MFResolvingClientSample
{
    public class Program
    {
        public static void Main()
        {
            using (ResolvingClient client = new ResolvingClient())
            {
                client.PrintResolveMatchInfo();
            }
        }
    }
}
