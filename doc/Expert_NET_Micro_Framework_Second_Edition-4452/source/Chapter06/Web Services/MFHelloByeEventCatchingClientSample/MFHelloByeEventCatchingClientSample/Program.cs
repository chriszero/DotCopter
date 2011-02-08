using System.Threading;
using Dpws.Client;
using Dpws.Client.Discovery;
using Microsoft.SPOT;

namespace MFHelloByeEventCatchingClient
{
    public class Program
    {
        public static void Main()
        {
            using (DpwsClient client = new DpwsClient()) // initializing
            {
                // Set this client property if you want to ignore this devices request
                client.IgnoreRequestFromThisIP = false;
                client.HelloEvent += new HelloEventHandler(client_HelloEvent);
                client.ByeEvent += new ByeEventHandler(client_ByeEvent);

                // Keep the client alive
                Thread.Sleep(Timeout.Infinite);
            }
        }

        private static void client_HelloEvent(object obj, DpwsServiceDescription helloEventArgs)
        {
            // Print Hello event information
            Debug.Print("");
            Debug.Print("Hello Event:");

            Debug.Print("Endpoint Address = " +
                         helloEventArgs.Endpoint.Address.AbsoluteUri);

            Debug.Print("Types:");
            for (int t = 0; t < helloEventArgs.ServiceTypes.Count; ++t)
            {
                DpwsServiceType serviceType = helloEventArgs.ServiceTypes[t];
                Debug.Print("\tName = " + serviceType.TypeName);
                Debug.Print("\tNamespace = " + serviceType.NamespaceUri);
                Debug.Print("");
            }

            Debug.Print("XAddrs:");
            foreach (string xaddr in helloEventArgs.XAddrs)
                Debug.Print("\tTransport Address = " + xaddr);

            Debug.Print("Metadata Version = " +
                         helloEventArgs.MetadataVersion);
        }

        private static void client_ByeEvent(object obj, DpwsServiceDescription byeEventArgs)
        {
            // Print Bye event information
            Debug.Print("");
            Debug.Print("Bye Event:");

            Debug.Print("Endpoint Address = " +
                         byeEventArgs.Endpoint.Address.AbsoluteUri);

            Debug.Print("XAddrs:");
            foreach (string xaddr in byeEventArgs.XAddrs)
                Debug.Print("\tTransport Address = " + xaddr);
        }
    }
}
