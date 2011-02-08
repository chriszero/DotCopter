using System;
using Microsoft.SPOT;
using Dpws.Client;
using Dpws.Client.Discovery;

namespace MFResolvingClientSample
{
    public class ResolvingClient : DpwsClient
    {
        public void PrintResolveMatchInfo()
        {
            Debug.Print("Resolving a device...");
            string endpointAddr = "urn:uuid:c5201073-fa27-c8c9-9634-0000001dd159";
            DpwsServiceDescription resolveMatch = this.DiscoveryClient.Resolve(endpointAddr);
            if (resolveMatch != null)
            {
                // Print resolve match information
                Debug.Print("");
                Debug.Print("Resolve Match:");

                Debug.Print("Endpoint Address = " +
                             resolveMatch.Endpoint.Address.AbsoluteUri);

                Debug.Print("Types:");
                for (int t = 0; t < resolveMatch.ServiceTypes.Count; ++t)
                {
                    DpwsServiceType matchType = resolveMatch.ServiceTypes[t];
                    Debug.Print("\tName = " + matchType.TypeName);
                    Debug.Print("\tNamespace = " + matchType.NamespaceUri);
                    Debug.Print("");
                }

                Debug.Print("XAddrs:");
                foreach (string xaddr in resolveMatch.XAddrs)
                    Debug.Print("\tTransport Address = " + xaddr);

                Debug.Print("Metadata Version = " +
                             resolveMatch.MetadataVersion);
            }
            else
                Debug.Print("Device cannot be resolved.");
        }
    }
}
