using System;
using System.Xml;
using Dpws.Client;
using Dpws.Client.Eventing;
using Microsoft.SPOT;
using Ws.Services;
using Ws.Services.WsaAddressing;

namespace MFEventingClientSample
{
    public class EventingServiceController : DpwsClient
    {
        internal const string c_namespaceUri = "http://schemas.sample.org/EventingService";
        internal const string c_serviceTypeName = "EventingServiceType";
        private const string c_namespacePrefix = "eventing";

        private readonly string serviceTransportAddress;

        public EventingServiceController(string serviceTransportAddress)
        {
            this.serviceTransportAddress = serviceTransportAddress;

            // Set this device property if you want to ignore this service notifications
            this.IgnoreRequestFromThisIP = false;

            // Assigning either a static or random endpoint address will
            // init the transport address of this client which we will use soon
            this.EndpointAddress = "urn:uuid:" + Guid.NewGuid();

            // Adding a event handler
            this.ServiceOperations.Add(new WsServiceOperation(c_namespaceUri, // namespace
                                                              "SimpleEvent")); // event (method) name

            // Subscribe to the event
            DpwsServiceType subscriptionType = new DpwsServiceType("SimpleEvent", c_namespaceUri);
            DpwsSubscribeRequest request = new DpwsSubscribeRequest(subscriptionType, // subscription type
                                                                    serviceTransportAddress, // event source address
                                                                    this.TransportAddress, // notify to address
                                                                    null, // expires
                                                                    null // event identifier
                                                                    );
           this.EventingClient.Subscribe(request);
        }

        private byte[] SimpleEvent(WsWsaHeader header, XmlReader reader)
        {
            reader.ReadStartElement("SimpleEvent", c_namespaceUri);

            // Extract parameter Quotient from SOAP message body
            string str = reader.ReadElementString("A", c_namespaceUri);
            int a = Convert.ToInt32(str);
            Debug.Print("Simple event notification was received with A=" +
                        a + ".");

            // Event handlers provide no response (but do not return null).
            return new byte[0]; 
        }
    }
}
