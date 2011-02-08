using System.Threading;
using Dpws.Device;

namespace MFEventingDeviceSample
{
    public class Program
    {
        public static void Main()
        {
            // Let clients identify this device
            Device.EndpointAddress = "urn:uuid:a56d1964-0906-c8ca-ade5-0000009c2992";
            // Add service as hosted service
            EventingService eventingService = new EventingService();
            Device.HostedServices.Add(eventingService);

            // Metadata
            // Model
            Device.ThisModel.Manufacturer = "Apress, Inc.";
            Device.ThisModel.ManufacturerUrl = "http://www.apress.com";
            Device.ThisModel.ModelName = "EventingModel";
            Device.ThisModel.ModelNumber = "12345";
            Device.ThisModel.ModelUrl = "http://www.apress.com";
            Device.ThisModel.PresentationUrl = "http://www.apress.com";
            // Device
            Device.ThisDevice.FriendlyName = "Eventing service device";
            Device.ThisDevice.FirmwareVersion = "demo";
            Device.ThisDevice.SerialNumber = "12345678";

            // Set this device property if you want to ignore this clients request
            Device.IgnoreLocalClientRequest = false;

            // Start the device
            Device.Start();

            int a = 0;
            while (true)
            {
                eventingService.FireSimpleEvent(a);
                a++;
                Thread.Sleep(1000);
            }
        }
    }
}
