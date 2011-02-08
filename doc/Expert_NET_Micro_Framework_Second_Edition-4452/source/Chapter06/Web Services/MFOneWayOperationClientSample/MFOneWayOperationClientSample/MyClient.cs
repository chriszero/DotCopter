using System;
using Dpws.Client;
using Dpws.Client.Discovery;
using Microsoft.SPOT;

namespace MFOneWayOperationClientSample
{
    public class MyClient : DpwsClient
    {
        /// <summary>
        /// Looks for devices that host a service identified by its type and namespace. 
        /// It returns the transport address of the first matching service.
        /// </summary>
        public string FindFirst(string serviceTypeName, string namespaceUri)
        {
            if (serviceTypeName == null)
                throw new ArgumentNullException();
            if (namespaceUri == null)
                throw new ArgumentNullException();
            
            Debug.Print("Discovering service devices...");
            // Define search criterias
            DpwsServiceType serviceType = new DpwsServiceType(serviceTypeName, namespaceUri);
            DpwsServiceTypes filters = new DpwsServiceTypes();
            filters.Add(serviceType);
            // Probe for devices
            DpwsServiceDescriptions probeMatches = this.DiscoveryClient.Probe(filters);
            if (probeMatches != null && probeMatches.Count > 0)
            {
                // Remember transport address of the first device
                string deviceTransportAddress = probeMatches[0].XAddrs[0];
                // Request metadata to get the desired service and its ID
                DpwsMexClient mexClient = new DpwsMexClient();
                DpwsMetadata metadata = mexClient.Get(deviceTransportAddress);
                // Check host service
                DpwsMexService host = metadata.Relationship.Host;
                if (host != null) // has host service
                {
                    if (host.ServiceTypes[serviceTypeName] != null)
                        return host.EndpointRefs[0].Address.AbsoluteUri;
                }
                // Check hosted services
                DpwsMexServices hostedServices = metadata.Relationship.HostedServices;
                if (hostedServices != null)
                {
                    for(int i = 0; i < hostedServices.Count; ++i)
                    {
                        DpwsMexService hostedService = hostedServices[i];
                        if (hostedService.ServiceTypes[serviceTypeName] != null)
                            return hostedService.EndpointRefs[0].Address.AbsoluteUri;
                    }
                }
            }
            Debug.Print("No service found.");
            return null;
        }
    }
}
