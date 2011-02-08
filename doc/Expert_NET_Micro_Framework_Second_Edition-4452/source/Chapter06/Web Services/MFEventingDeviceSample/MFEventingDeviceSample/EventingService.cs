using Dpws.Device;
using Dpws.Device.Services;
using Microsoft.SPOT;
using Ws.Services.WsaAddressing;
using Ws.Services.Xml;

namespace MFEventingDeviceSample
{
    public class EventingService : DpwsHostedService
    {
        internal const string c_namespaceUri = "http://schemas.sample.org/EventingService";

        public EventingService()
        {
            // The client filters devices when probing using the ServiceTypeName property
            this.ServiceTypeName = "EventingServiceType";
            // Add a unique namespace for our service
            this.ServiceNamespace = new WsXmlNamespace("eventing", // prefix
                                                       c_namespaceUri); // URI
            // Unique ID to access the service
            this.ServiceID = "urn:uuid:93252386-0724-c8ca-bd31-000000732d93";

            // Add event source
            DpwsWseEventSource eventSource =
                new DpwsWseEventSource(this.ServiceNamespace.Prefix,
                                       this.ServiceNamespace.NamespaceURI,
                                       "SimpleEvent"); // event name
            this.EventSources.Add(eventSource);
        }

        public void FireSimpleEvent(int a)
        {
            DpwsWseEventSource eventSource = this.EventSources["SimpleEvent"];
            WsWsaHeader header = new WsWsaHeader(c_namespaceUri + "/SimpleEvent", null, null, null, null, null);
            Device.SubscriptionManager.FireEvent(this, // hosted service
                                                 eventSource,
                                                 header,
                                                 BuildSimpleEventMessageBody(a)
                                                 );
            Debug.Print("Simple event was fired with param a=" + a + ".");
        }

        // Build integer event
        private string BuildSimpleEventMessageBody(int a)
        {
            return "<evnt:SimpleEvent xmlns:evnt='" + c_namespaceUri + "'>" +
                     "<evnt:A>" + a + "</evnt:A>" +
                   "</evnt:SimpleEvent>";
        }
    }
}
