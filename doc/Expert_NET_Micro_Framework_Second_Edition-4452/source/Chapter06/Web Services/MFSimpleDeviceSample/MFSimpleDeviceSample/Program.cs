using System.Threading;
using Dpws.Device;
using Dpws.Device.Services;
using Ws.Services.Xml;

namespace MFSimpleDeviceSample
{
    public class Program
    {
        public static void Main()
        {
            // *** Relationship (Services) ***
            // Set a simple service without any 
            // operations and event as host service  
            DpwsHostedService service1 = new DpwsHostedService();
            service1.ServiceTypeName = "SimpleServiceType1";
            service1.ServiceNamespace = new WsXmlNamespace("simple1", // prefix
                                                           "http://schemas.sample.org/SimpleService1"); // namespace URI
            service1.ServiceID = "urn:uuid:cf16db78-02c9-c8ca-b37b-0000004071f6";
            // Set the service as host service
            Device.Host = service1;

            // Add a second service as hosted service
            DpwsHostedService service2 = new DpwsHostedService();
            service2.ServiceTypeName = "SimpleServiceType2";
            service2.ServiceNamespace = new WsXmlNamespace("simple2", // prefix
                                                           "http://schemas.sample.org/SimpleService2"); // namespace URI
            service2.ServiceID = "urn:uuid:ec499d62-02c9-c8ca-b7ee-0000000bf3dd";
            // Add the service as hosted service
            Device.HostedServices.Add(service2);

            // Let clients identify this device
            Device.EndpointAddress = "urn:uuid:c5201073-fa27-c8c9-9634-0000001dd159";
            // Set this device property if you want to ignore this clients request
            Device.IgnoreLocalClientRequest = false;

            // Start the device
            Device.Start();

            // Keep the device alive
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
