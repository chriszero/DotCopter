using System;
using System.Ext.Xml;
using System.IO;
using System.Text;
using System.Xml;
using Dpws.Client;
using Dpws.Client.Transport;
using Microsoft.SPOT;
using Ws.Services;

namespace MFTwoWayOperationClientSample
{
    public class TwoWayOperationServiceController : DpwsClient
    {
        internal const string c_namespaceUri = "http://schemas.sample.org/TwoWayOperationService";
        internal const string c_serviceTypeName = "TwoWayOperationServiceType";
        private const string c_namespacePrefix = "twoWay";

        private readonly string serviceTransportAddress;

        public TwoWayOperationServiceController(string serviceTransportAddress)
        {
            this.serviceTransportAddress = serviceTransportAddress;
        }

        public int MyTwoWayOperation(int a, int b)
        {
            // Create HttpClient and send request
            Debug.Print("Sending Request:");
            byte[] request = BuildMyTwoWayRequest(a, b);
            Debug.Print(new string(Encoding.UTF8.GetChars(request)));
            DpwsHttpClient httpClient = new DpwsHttpClient();
            DpwsSoapResponse response =
                         httpClient.SendRequest(request, // soap message
                                                this.serviceTransportAddress,
                                                false, //is one way?
                                                false // is chunked?
                                               );
            if (response == null)
                throw new InvalidOperationException("Two-way response was null.");
            try
            {
                return ParseTwoWayResponse(response.Reader);
            }
            finally
            {
                response.Reader.Close();
            }
        }


        private byte[] BuildMyTwoWayRequest(int a, int b)
        {
            MemoryStream soapStream = new MemoryStream();
            XmlWriter xmlWriter = XmlWriter.Create(soapStream);

            // Write processing instructions and root element
            xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
            xmlWriter.WriteStartElement("soap", "Envelope", WsWellKnownUri.SoapNamespaceUri);

            // Write namespaces
            xmlWriter.WriteAttributeString("xmlns",
                                           "wsa",
                                           null,
                                           WsWellKnownUri.WsaNamespaceUri);
            // Write our namespace
            xmlWriter.WriteAttributeString("xmlns",
                                           c_namespacePrefix,
                                           null,
                                           c_namespaceUri);

            // Write header
            xmlWriter.WriteStartElement("soap", "Header", null);
            xmlWriter.WriteStartElement("wsa", "To", null);
            xmlWriter.WriteString(this.serviceTransportAddress);
            xmlWriter.WriteEndElement(); // End To
            // Action indicates the desired operation to execute
            xmlWriter.WriteStartElement("wsa", "Action", null);
            xmlWriter.WriteString(c_namespaceUri + "/" + "MyTwoWayOperation");
            xmlWriter.WriteEndElement(); // End Action
            xmlWriter.WriteStartElement("wsa", "From", null);
            xmlWriter.WriteStartElement("wsa", "Address", null);
            xmlWriter.WriteString(this.EndpointAddress);
            xmlWriter.WriteEndElement(); // End Address
            xmlWriter.WriteEndElement(); // End From
            xmlWriter.WriteStartElement("wsa", "MessageID", null);
            xmlWriter.WriteString("urn:uuid:" + Guid.NewGuid());
            xmlWriter.WriteEndElement(); // End MessageID
            xmlWriter.WriteEndElement(); // End Header

            // write body
            xmlWriter.WriteStartElement("soap", "Body", null);
            // This is the container for our data
            xmlWriter.WriteStartElement(c_namespacePrefix,
                                        "MyTwoWayRequest", null);
            // The first parameter value
            xmlWriter.WriteStartElement(c_namespacePrefix,
                                        "A", null);
            xmlWriter.WriteString(a.ToString());
            xmlWriter.WriteEndElement(); // End A
            // The second parameter value
            xmlWriter.WriteStartElement(c_namespacePrefix,
                                        "B", null);
            xmlWriter.WriteString(b.ToString());
            xmlWriter.WriteEndElement(); // End B
            xmlWriter.WriteEndElement(); // End MyTwoWayRequest
            xmlWriter.WriteEndElement(); // End Body

            xmlWriter.WriteEndElement();

            // Create return buffer and close writer
            xmlWriter.Flush();
            byte[] soapBuffer = soapStream.ToArray();
            xmlWriter.Close();

            return soapBuffer;
        }

        private int ParseTwoWayResponse(XmlReader reader)
        {
            reader.ReadStartElement("MyTwoWayResponse", c_namespaceUri);

            // Extract parameter Quotient from SOAP message body
            string str = reader.ReadElementString("Quotient", c_namespaceUri);
            int quotient = Convert.ToInt32(str);
            return quotient;
        }
    }
}
