using Microsoft.SPOT;

namespace MFTwoWayOperationClientSample
{
    public class Program
    {
        public static void Main()
        {
            // Discover a two-way operation service
            using (MyClient client = new MyClient())
            {
                string serviceTransportAddress = client.FindFirst(TwoWayOperationServiceController.c_serviceTypeName,
                                                                  TwoWayOperationServiceController.c_namespaceUri);
                if (serviceTransportAddress != null)
                {
                    TwoWayOperationServiceController controller = new TwoWayOperationServiceController(serviceTransportAddress);
                    // Service found, invoke two-way OP
                    // Calculate 35 / 7
                    int quotient = controller.MyTwoWayOperation(35, 7);
                    Debug.Print("Quotient=" + quotient);
                }
            }
        }
    }
}
