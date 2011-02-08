using Dpws.Client;
using Dpws.Client.Discovery;
using Microsoft.SPOT;

namespace MFMetadataGettingClientSample
{
    public class MetadataGettingClient : DpwsClient
    {
        public void PrintMetadataInfo()
        {
            this.IgnoreRequestFromThisIP = true;

            // This is the endpoint (logical) address of the target device
            // we want to obtain the metadata
            string deviceEndpointAddr = "urn:uuid:bde0943a-0516-c8ca-80a6-000000b525ed";

            Debug.Print("Resolving the device...");
            // We need to resolve the device to get the transport address
            DpwsServiceDescription resolveMatch = this.DiscoveryClient.Resolve(deviceEndpointAddr);
            if (resolveMatch != null)
            {
                // Device was located
                string deviceTransportAddr = resolveMatch.XAddrs[0];

                // Get metadata
                DpwsMexClient mexClient = new DpwsMexClient();
                DpwsMetadata metadata = mexClient.Get(deviceTransportAddr);
                if (metadata != null)
                {
                    Debug.Print("");
                    Debug.Print("Metadata:");
                    Debug.Print("ThisModel:");
                    Debug.Print("\tManufacturer: " + metadata.ThisModel.Manufacturer);
                    Debug.Print("\tManufacturerUrl: " + metadata.ThisModel.ManufacturerUrl);
                    Debug.Print("\tModelName: " + metadata.ThisModel.ModelName);
                    Debug.Print("\tModelNumber: " + metadata.ThisModel.ModelNumber);
                    Debug.Print("\tModelUrl: " + metadata.ThisModel.ModelUrl);
                    Debug.Print("\tPresentationUrl: " + metadata.ThisModel.PresentationUrl);
                    Debug.Print("ThisDevice:");
                    Debug.Print("\tFirmwareVersion: " + metadata.ThisDevice.FirmwareVersion);
                    Debug.Print("\tFriendlyName: " + metadata.ThisDevice.FriendlyName);
                    Debug.Print("\tSerialNumber: " + metadata.ThisDevice.SerialNumber);
                    DpwsMexService host = metadata.Relationship.Host;
                    if (host != null)
                    {
                        Debug.Print("Host:");
                        Debug.Print("\tServiceID: " + host.ServiceID);
                        Debug.Print("\tAddress: " + host.EndpointRefs[0].Address.AbsoluteUri);
                        Debug.Print("\tTypes:");
                        for (int t = 0; t < host.ServiceTypes.Count; ++t)
                        {
                            DpwsServiceType serviceType = host.ServiceTypes[t];
                            Debug.Print("\t\tName = " + serviceType.TypeName);
                            Debug.Print("\t\tNamespace = " + serviceType.NamespaceUri);
                            Debug.Print("");
                        }
                    }
                    DpwsMexServices hostedServices = metadata.Relationship.HostedServices;
                    if (hostedServices != null)
                    {
                        Debug.Print("HostedServices:");
                        for (int i = 0; i < hostedServices.Count; i++)
                        {
                            DpwsMexService hostedService = hostedServices[i];
                            Debug.Print("\tService ID: " + hostedService.ServiceID);
                            Debug.Print("\tAddress: " + hostedService.EndpointRefs[0].Address.AbsoluteUri);
                            Debug.Print("\tTypes:");
                            for (int t = 0; t < hostedService.ServiceTypes.Count; ++t)
                            {
                                DpwsServiceType serviceType = hostedService.ServiceTypes[t];
                                Debug.Print("\t\tName = " + serviceType.TypeName);
                                Debug.Print("\t\tNamespace = " + serviceType.NamespaceUri);
                                Debug.Print("");
                            }
                        }
                    }
                }
                else
                    Debug.Print("Did not get metadata from device.");
            }
            else
                Debug.Print("Device cannot be resolved.");
        }
    }
}
