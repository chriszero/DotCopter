using System;
using System.Ext.Xml;
using System.IO;
using System.Text;
using Dpws.Client;
using Dpws.Client.Transport;
using Microsoft.SPOT;
using Ws.Services;

namespace MFOneWayOperationClientSample
{
    public class OneWayOperationServiceController : DpwsClient
    {
        internal const string c_namespaceUri = "http://schemas.sample.org/OneWayOperationService";
        internal const string c_serviceTypeName = "OneWayOperationServiceType";
        private const string c_namespacePrefix = "oneWay";

        private readonly string serviceTransportAddress;

        public OneWayOperationServiceController(string serviceTransportAddress)
        {
            this.serviceTransportAddress = serviceTransportAddress;
        }

        public void MyOneWayOperation(int a)
        {
            // Create HttpClient and send request
            Debug.Print("Sending Request:");
            byte[] request = BuildMyOneWayRequest(a);
            Debug.Print(new string(Encoding.UTF8.GetChars(request)));
            DpwsHttpClient httpClient = new DpwsHttpClient();
            httpClient.SendRequest(request, // soap message
                                   this.serviceTransportAddress,
                                   true, //is one-way?
                                   false // is chunked?
                                   );
        }


        private byte[] BuildMyOneWayRequest(int a)
        {
            MemoryStream soapStream = new MemoryStream();
            XmlWriter xmlWriter = XmlWriter.Create(soapStream);

            // Write processing instructions and root element
            xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
            xmlWriter.WriteStartElement("soap", "Envelope", WsWellKnownUri.SoapNamespaceUri);

            // Write namespaces
            xmlWriter.WriteAttributeString("xmlns", "wsa", null, WsWellKnownUri.WsaNamespaceUri);
            // Write our namespace
            xmlWriter.WriteAttributeString("xmlns", c_namespacePrefix, null, c_namespaceUri);

            // Write header
            xmlWriter.WriteStartElement("soap", "Header", null);
            xmlWriter.WriteStartElement("wsa", "To", null);
            xmlWriter.WriteString(this.serviceTransportAddress);
            xmlWriter.WriteEndElement(); // End To
            // Action indicates the desired operation to execute
            xmlWriter.WriteStartElement("wsa", "Action", null);
            xmlWriter.WriteString(c_namespaceUri + "/" + "MyOneWayOperation");
            xmlWriter.WriteEndElement(); // End Action
            xmlWriter.WriteStartElement("wsa", "From", null);
            xmlWriter.WriteStartElement("wsa", "Address", null);
            xmlWriter.WriteString(this.EndpointAddress); // client endpoint addr
            xmlWriter.WriteEndElement(); // End Address
            xmlWriter.WriteEndElement(); // End From
            xmlWriter.WriteStartElement("wsa", "MessageID", null);
            xmlWriter.WriteString("urn:uuid:" + Guid.NewGuid());
            xmlWriter.WriteEndElement(); // End MessageID
            xmlWriter.WriteEndElement(); // End Header

            // write body
            xmlWriter.WriteStartElement("soap", "Body", null);
            // This is the container for our data
            xmlWriter.WriteStartElement(c_namespacePrefix, "MyOneWayRequest", null);
            // The actual parameter value
            xmlWriter.WriteStartElement(c_namespacePrefix, "A", null);
            xmlWriter.WriteString(a.ToString());
            xmlWriter.WriteEndElement(); // End A
            xmlWriter.WriteEndElement(); // End MyOneWayRequest
            xmlWriter.WriteEndElement(); // End Body

            xmlWriter.WriteEndElement();

            // Create return buffer and close writer
            xmlWriter.Flush();
            byte[] soapBuffer = soapStream.ToArray();
            xmlWriter.Close();

            return soapBuffer;
        }
    }
}
