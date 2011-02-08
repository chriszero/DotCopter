using System.Threading;

namespace MFEventingClientSample
{
    public class Program
    {
        public static void Main()
        {
            // Discover an eventing service
            using (MyClient client = new MyClient())
            {
                string serviceTransportAddress = client.FindFirst(EventingServiceController.c_serviceTypeName,
                                                                  EventingServiceController.c_namespaceUri);
                if (serviceTransportAddress != null)
                {
                    EventingServiceController controller = new EventingServiceController(serviceTransportAddress);
                    Thread.Sleep(Timeout.Infinite);
                }
            }
        }
    }
}
