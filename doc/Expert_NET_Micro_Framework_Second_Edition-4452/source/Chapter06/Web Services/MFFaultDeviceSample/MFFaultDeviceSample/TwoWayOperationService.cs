using System;
using System.Ext.Xml;
using System.IO;
using System.Xml;
using Dpws.Device.Services;
using Microsoft.SPOT;
using Ws.Services;
using Ws.Services.Faults;
using Ws.Services.WsaAddressing;
using Ws.Services.Xml;

namespace MFTwoWayOperationDeviceSample
{
    public class TwoWayOperationService : DpwsHostedService
    {
        private const string c_namespaceUri = "http://schemas.sample.org/TwoWayOperationService";

        public TwoWayOperationService()
        {
            // The client filters devices when probing using the ServiceTypeName property
            this.ServiceTypeName = "TwoWayOperationServiceType";
            // Add a unique namespace for our service
            this.ServiceNamespace = new WsXmlNamespace("twoWay", // prefix
                                                       c_namespaceUri); // URI
            // Unique ID to access the service
            this.ServiceID = "urn:uuid:93252386-0724-c8ca-bd31-000000732d93";
            // Add the two-way service operation
            WsServiceOperation operation =
                     new WsServiceOperation(this.ServiceNamespace.NamespaceURI, // namespace
                                            "MyTwoWayOperation");               // operation method name
            this.ServiceOperations.Add(operation);
        }

        public byte[] MyTwoWayOperation(WsWsaHeader header, XmlReader reader)
        {
            try
            {
                reader.ReadStartElement("MyTwoWayRequest", this.ServiceNamespace.NamespaceURI);

                // Extract parameter A from SOAP message body
                string strA = reader.ReadElementString("A", this.ServiceNamespace.NamespaceURI);

                int a = Convert.ToInt32(strA);
                // Extract parameter B from SOAP message body
                string strB = reader.ReadElementString("B", this.ServiceNamespace.NamespaceURI);
                int b = Convert.ToInt32(strB);
                if (b == 0)
                    throw new WsFaultException(header, WsFaultType.ArgumentException, "Division by zero. Argument B must not be zero.");

                Debug.Print("MyTwoWayOperation with A=" + a + " / B=" + b + " executed.");
                int quotient = a / b;
                Debug.Print("Operation returns " + quotient + ".");
                return BuildMyTwoWayResponse(header, quotient);
            }
            catch (XmlException ex)
            {
                throw new WsFaultException(header, WsFaultType.XmlException, ex.Message);
            }
            catch (WsFaultException)
            {
                throw; // rethrow fault exception as it is
            }
            catch (Exception ex) // all other exception types
            {
                throw new WsFaultException(header, WsFaultType.Exception, ex.Message);
            }
        }

        public byte[] BuildMyTwoWayResponse(WsWsaHeader header, int quotient)
        {
            MemoryStream soapStream = new MemoryStream();
            XmlWriter xmlWriter = XmlWriter.Create(soapStream);

            // Write processing instructions and root element
            xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
            xmlWriter.WriteStartElement("soap", "Envelope", WsWellKnownUri.SoapNamespaceUri);

            // Write namespaces
            xmlWriter.WriteAttributeString("xmlns", "wsa", null, WsWellKnownUri.WsaNamespaceUri);
            xmlWriter.WriteAttributeString("xmlns", "twoWay", null, c_namespaceUri);

            // Write header
            xmlWriter.WriteStartElement("soap", "Header", null);
            xmlWriter.WriteStartElement("wsa", "To", null);
            xmlWriter.WriteString(header.From.Address.AbsoluteUri);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("wsa", "Action", null);
            xmlWriter.WriteString(c_namespaceUri + "/TwoWayOperationResponse");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("wsa", "RelatesTo", null);
            xmlWriter.WriteString(header.MessageID);
            xmlWriter.WriteEndElement(); // End RelatesTo
            xmlWriter.WriteStartElement("wsa", "MessageID", null);
            xmlWriter.WriteString("urn:uuid:" + Guid.NewGuid());
            xmlWriter.WriteEndElement(); // End MessageID
            xmlWriter.WriteEndElement(); // End Header

            // write body
            xmlWriter.WriteStartElement("soap", "Body", null);
            xmlWriter.WriteStartElement("twoWay", "MyTwoWayResponse", null);
            xmlWriter.WriteStartElement("twoWay", "Quotient", null);
            xmlWriter.WriteString(quotient.ToString());
            xmlWriter.WriteEndElement(); // End Quotient
            xmlWriter.WriteEndElement(); // End MyTwoWayResponse
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
