using System;

namespace MFOneWayOperationClientSample
{
    public class Program
    {
        public static void Main()
        {
            // Discover a one-way operation service
            using (MyClient client = new MyClient())
            {
                string serviceTransportAddress = client.FindFirst(OneWayOperationServiceController.c_serviceTypeName,
                                                                  OneWayOperationServiceController.c_namespaceUri);
                if (serviceTransportAddress != null)
                {
                    OneWayOperationServiceController controller = new OneWayOperationServiceController(serviceTransportAddress);
                    // Service found, invoke one-way OP with argument 99
                    controller.MyOneWayOperation(99);
                }
            }
        }
    }
}
