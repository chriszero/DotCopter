using System.Threading;
using Dpws.Device;
using Dpws.Device.Services;
using Ws.Services.Xml;

namespace MFMetadataProvidingDeviceSample
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
            service1.ServiceNamespace = new WsXmlNamespace("simple1", "http://schemas.sample.org/SimpleService1");
            service1.ServiceID = "urn:uuid:cf16db78-02c9-c8ca-b37b-0000004071f6";
            // set the service as host service
            Device.Host = service1;
            //
            // Add a second service as hosted service
            DpwsHostedService service2 = new DpwsHostedService();
            service2.ServiceTypeName = "SimpleServiceType2";
            service2.ServiceNamespace = new WsXmlNamespace("simple2", "http://schemas.sample.org/SimpleService2");
            service2.ServiceID = "urn:uuid:ec499d62-02c9-c8ca-b7ee-0000000bf3dd";
            // Add the service as hosted service
            Device.HostedServices.Add(service2);

            // Let clients identify this device
            Device.EndpointAddress = "urn:uuid:bde0943a-0516-c8ca-80a6-000000b525ed";
            // Set this device property if you want to ignore this clients request
            Device.IgnoreLocalClientRequest = false;

            // Metadata
            // ThisModel
            Device.ThisModel.Manufacturer = "Apress, Inc.";
            Device.ThisModel.ManufacturerUrl = "http://www.apress.com";
            Device.ThisModel.ModelName = "MetadataProvidingModel";
            Device.ThisModel.ModelNumber = "12345";
            Device.ThisModel.ModelUrl = "http://www.apress.com";
            Device.ThisModel.PresentationUrl = "http://www.apress.com";
            // ThisDevice
            Device.ThisDevice.FriendlyName = "Describing device that provides metadata";
            Device.ThisDevice.FirmwareVersion = "demo";
            Device.ThisDevice.SerialNumber = "12345678";

            // Start the device
            Device.Start();

            // Keep the device alive
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
