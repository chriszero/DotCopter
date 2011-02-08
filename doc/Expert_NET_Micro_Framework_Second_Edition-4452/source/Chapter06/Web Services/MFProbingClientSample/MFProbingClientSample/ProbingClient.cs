using Dpws.Client;
using Dpws.Client.Discovery;
using Microsoft.SPOT;

namespace MFProbingClientSample
{
    public class ProbingClient : DpwsClient
    {
        public void PrintProbeMatchInfo()
        {
            Debug.Print("Discovering service devices...");
            // Define search criterias
            DpwsServiceType serviceType = new DpwsServiceType("SimpleServiceType2", // type name
                                                              "http://schemas.sample.org/SimpleService2"); // namespace URI
            DpwsServiceTypes filters = new DpwsServiceTypes();
            filters.Add(serviceType);
            // Probe for devices
            DpwsServiceDescriptions probeMatches = this.DiscoveryClient.Probe(filters);
            if (probeMatches != null)
            {
                for (int i = 0; i < probeMatches.Count; ++i)
                {
                    DpwsServiceDescription probeMatch = probeMatches[i];
                    // Print probe match information
                    Debug.Print("");
                    Debug.Print("Probe Match:");

                    Debug.Print("Endpoint Address = " + probeMatch.Endpoint.Address.AbsoluteUri);

                    Debug.Print("Types:");
                    for (int t = 0; t < probeMatch.ServiceTypes.Count; ++t)
                    {
                        DpwsServiceType matchType = probeMatch.ServiceTypes[t];
                        Debug.Print("\tName = " + matchType.TypeName);
                        Debug.Print("\tNamespace = " + matchType.NamespaceUri);
                        Debug.Print("");
                    }

                    Debug.Print("XAddrs:");
                    foreach (string xaddr in probeMatch.XAddrs)
                        Debug.Print("\tTransport Address = " + xaddr);

                    Debug.Print("Metadata Version = " + probeMatch.MetadataVersion);
                }
            }
            else
                Debug.Print("No service device found.");
        }
    }
}
