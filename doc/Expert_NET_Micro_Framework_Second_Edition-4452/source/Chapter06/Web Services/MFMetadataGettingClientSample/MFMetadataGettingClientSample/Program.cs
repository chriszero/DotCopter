using System;

namespace MFMetadataGettingClientSample
{
    public class Program
    {
        public static void Main()
        {
            using (MetadataGettingClient client = new MetadataGettingClient())
            {
                client.PrintMetadataInfo();
            }
        }
    }
}
