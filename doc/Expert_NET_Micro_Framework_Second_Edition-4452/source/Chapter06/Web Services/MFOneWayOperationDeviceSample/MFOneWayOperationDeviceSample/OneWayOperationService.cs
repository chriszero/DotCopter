using System;
using System.Xml;
using Dpws.Device.Services;
using Microsoft.SPOT;
using Ws.Services;
using Ws.Services.WsaAddressing;
using Ws.Services.Xml;

namespace MFOneWayOperationDeviceSample
{
    public class OneWayOperationService : DpwsHostedService
    {
        public OneWayOperationService()
        {
            // The client filters devices when probing using the ServiceTypeName property
            this.ServiceTypeName = "OneWayOperationServiceType";
            // Add a unique namespace for our service
            this.ServiceNamespace = new WsXmlNamespace("oneWay", // prefix
                                                       "http://schemas.sample.org/OneWayOperationService"); // URI
            // Unique ID to access the service
            this.ServiceID = "urn:uuid:93252386-0724-c8ca-bd31-000000732d93";
            // Add the one-way service operation
            WsServiceOperation operation =
                     new WsServiceOperation(this.ServiceNamespace.NamespaceURI, // namespace
                                            "MyOneWayOperation");               // operation method name
            this.ServiceOperations.Add(operation);
        }

        public byte[] MyOneWayOperation(WsWsaHeader header, XmlReader reader)
        {
            reader.ReadStartElement("MyOneWayRequest", this.ServiceNamespace.NamespaceURI);

            // Extract parameter A from SOAP message body
            string str = reader.ReadElementString("A", this.ServiceNamespace.NamespaceURI);
            int a = Convert.ToInt32(str);

            Debug.Print("MyOneWayOperation with A=" + a + " executed.");
            return null; // No response it is a one-way operation
        }
    }
}
