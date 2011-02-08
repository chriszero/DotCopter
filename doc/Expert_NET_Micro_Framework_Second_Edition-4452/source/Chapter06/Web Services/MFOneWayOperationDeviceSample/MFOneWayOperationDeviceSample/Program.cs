using System.Threading;
using Dpws.Device;

namespace MFOneWayOperationDeviceSample
{
    public class Program
    {
        public static void Main()
        {
            // Let clients identify this device
            Device.EndpointAddress = "urn:uuid:e7819a89-0065-c8ca-af49-000000a858a6";
            // Add service as hosted service
            Device.HostedServices.Add(new OneWayOperationService());

            // Metadata
            // Model
            Device.ThisModel.Manufacturer = "Apress, Inc.";
            Device.ThisModel.ManufacturerUrl = "http://www.apress.com";
            Device.ThisModel.ModelName = "OneWayOperationModel";
            Device.ThisModel.ModelNumber = "12345";
            Device.ThisModel.ModelUrl = "http://www.apress.com";
            Device.ThisModel.PresentationUrl = "http://www.apress.com";
            // Device
            Device.ThisDevice.FriendlyName = "One-way operation service device";
            Device.ThisDevice.FirmwareVersion = "demo";
            Device.ThisDevice.SerialNumber = "12345678";

            // Set this device property if you want to ignore this clients request
            Device.IgnoreLocalClientRequest = false;

            // Start the device
            Device.Start();

            // Keep the device alive
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
