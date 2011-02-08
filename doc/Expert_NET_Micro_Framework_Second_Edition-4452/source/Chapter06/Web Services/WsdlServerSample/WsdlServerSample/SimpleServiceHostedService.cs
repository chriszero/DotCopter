using System;
using System.Text;
using System.Xml;
using Dpws.Device;
using Dpws.Device.Services;
using Ws.Services;
using Ws.Services.WsaAddressing;
using Ws.Services.Xml;

namespace schemas.example.org.SimpleService
{
    
    
    public class SimpleService : DpwsHostedService
    {
        
        private ISimpleService m_service = null;
        
        public SimpleService(ISimpleService service)
        {
            // Set the service implementation properties
            m_service = service;

            // Set base service properties
            ServiceNamespace = new WsXmlNamespace("sim", "http://schemas.example.org/SimpleService");
            ServiceID = "urn:uuid:6fa33842-ab2e-4eeb-b241-4f735013c4ec";
            ServiceTypeName = "SimpleService";

            // Add service types here
            ServiceOperations.Add(new WsServiceOperation("http://schemas.example.org/SimpleService", "OneWay"));
            ServiceOperations.Add(new WsServiceOperation("http://schemas.example.org/SimpleService", "TwoWayRequest"));
            ServiceOperations.Add(new WsServiceOperation("http://schemas.example.org/SimpleService", "TypeCheckRequest"));
            ServiceOperations.Add(new WsServiceOperation("http://schemas.example.org/SimpleService", "AnyCheckRequest"));

            // Add event sources here
        }
        
        public virtual Byte[] OneWay(WsWsaHeader header, XmlReader reader)
        {
            // Build request object
            OneWayDataContractSerializer reqDcs;
            reqDcs = new OneWayDataContractSerializer("OneWay", "http://schemas.example.org/SimpleService");
            OneWay req;
            req = ((OneWay)(reqDcs.ReadObject(reader)));

            // Call service operation to process request.
            m_service.OneWay(req);

            // Return null response for oneway messages
            return null;
        }
        
        public virtual Byte[] TwoWayRequest(WsWsaHeader header, XmlReader reader)
        {
            // Build request object
            TwoWayRequestDataContractSerializer reqDcs;
            reqDcs = new TwoWayRequestDataContractSerializer("TwoWayRequest", "http://schemas.example.org/SimpleService");
            TwoWayRequest req;
            req = ((TwoWayRequest)(reqDcs.ReadObject(reader)));

            // Create response object
            // Call service operation to process request and return response.
            TwoWayResponse resp;
            resp = m_service.TwoWay(req);

            // Create response header
            WsWsaHeader respHeader = new WsWsaHeader("http://schemas.example.org/SimpleService/TwoWayResponse", header.MessageID, WsWellKnownUri.WsaAnonymousUri, null, null, null);

            // Create response serializer
            TwoWayResponseDataContractSerializer respDcs;
            respDcs = new TwoWayResponseDataContractSerializer("TwoWayResponse", "http://schemas.example.org/SimpleService");

            // Build response message and return
            return SoapMessageBuilder.BuildSoapMessage(respHeader, respDcs, resp);
        }
        
        public virtual Byte[] TypeCheckRequest(WsWsaHeader header, XmlReader reader)
        {
            // Build request object
            TypeCheckRequestDataContractSerializer reqDcs;
            reqDcs = new TypeCheckRequestDataContractSerializer("TypeCheckRequest", "http://schemas.example.org/SimpleService");
            TypeCheckRequest req;
            req = ((TypeCheckRequest)(reqDcs.ReadObject(reader)));

            // Create response object
            // Call service operation to process request and return response.
            TypeCheckResponse resp;
            resp = m_service.TypeCheck(req);

            // Create response header
            WsWsaHeader respHeader = new WsWsaHeader("http://schemas.example.org/SimpleService/TypeCheckResponse", header.MessageID, WsWellKnownUri.WsaAnonymousUri, null, null, null);

            // Create response serializer
            TypeCheckResponseDataContractSerializer respDcs;
            respDcs = new TypeCheckResponseDataContractSerializer("TypeCheckResponse", "http://schemas.example.org/SimpleService");

            // Build response message and return
            return SoapMessageBuilder.BuildSoapMessage(respHeader, respDcs, resp);
        }
        
        public virtual Byte[] AnyCheckRequest(WsWsaHeader header, XmlReader reader)
        {
            // Build request object
            AnyCheckRequestDataContractSerializer reqDcs;
            reqDcs = new AnyCheckRequestDataContractSerializer("AnyCheckRequest", "http://schemas.example.org/SimpleService");
            AnyCheckRequest req;
            req = ((AnyCheckRequest)(reqDcs.ReadObject(reader)));

            // Create response object
            // Call service operation to process request and return response.
            AnyCheckResponse resp;
            resp = m_service.AnyCheck(req);

            // Create response header
            WsWsaHeader respHeader = new WsWsaHeader("http://schemas.example.org/SimpleService/AnyCheckResponse", header.MessageID, WsWellKnownUri.WsaAnonymousUri, null, null, null);

            // Create response serializer
            AnyCheckResponseDataContractSerializer respDcs;
            respDcs = new AnyCheckResponseDataContractSerializer("AnyCheckResponse", "http://schemas.example.org/SimpleService");

            // Build response message and return
            return SoapMessageBuilder.BuildSoapMessage(respHeader, respDcs, resp);
        }
    }
}
