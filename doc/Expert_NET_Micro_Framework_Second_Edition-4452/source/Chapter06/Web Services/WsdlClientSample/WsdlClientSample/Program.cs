using System;
using Dpws.Client;
using Dpws.Client.Discovery;
using Microsoft.SPOT;
using schemas.example.org.SimpleService;

namespace WsdlClientSample
{
    public class Program
    {
        private const string c_namespaceUri = "http://schemas.example.org/SimpleService";
        private const string c_serviceTypeName = "SimpleService";

        public static void Main()
        {
            using (SimpleServiceClientProxy client = new SimpleServiceClientProxy())
            {
                client.ServiceEndpoint = FindFirst(client.DiscoveryClient,
                                                   c_serviceTypeName,
                                                   c_namespaceUri);
                TwoWayRequest req = new TwoWayRequest();
                req.X = 3;
                req.Y = 4;
                TwoWayResponse resp = client.TwoWayRequest(req);
                Debug.Print("TwoWayRequest " + req.X + "+" + req.Y + "=" + resp.Sum);
            }
        }

        /// <summary>
        /// Looks for devices that host a service identified by its type and namespace. 
        /// It returns the transport address of the first matching service.
        /// </summary>
        public static string FindFirst(DpwsDiscoveryClient discoveryClient, string serviceTypeName, string namespaceUri)
        {
            if (discoveryClient == null)
                throw new ArgumentNullException();
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
            DpwsServiceDescriptions probeMatches = discoveryClient.Probe(filters);
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
                    for (int i = 0; i < hostedServices.Count; ++i)
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
