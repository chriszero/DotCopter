using System;
using System.Xml;
using Dpws.Client;
using Dpws.Client.Discovery;
using Dpws.Client.Eventing;
using Dpws.Client.Transport;
using Ws.Services;
using Ws.Services.Utilities;
using Ws.Services.WsaAddressing;
using Ws.Services.Xml;

namespace schemas.example.org.SimpleService
{
    
    
    public class SimpleServiceClientProxy : DpwsClient
    {
        
        public string ServiceEndpoint = null;
        
        public SimpleServiceClientProxy()
        {
            Init();
        }
        
        public SimpleServiceClientProxy(int port) : 
                base(port)
        {
            Init();
        }
        
        public virtual void Init()
        {

            // // Set client endpoint address
            EndpointAddress = "urn:uuid:e456a2aa-8ae1-4e3a-a6d9-26abf15e3847";
        }
        
        public virtual void OneWay(OneWay req)
        {

            // Create request header
            String action;
            action = "http://schemas.example.org/SimpleService/OneWay";
            WsWsaHeader header;
            header = new WsWsaHeader(action, null, ServiceEndpoint, null, EndpointAddress, null);

            // Create request serializer
            OneWayDataContractSerializer reqDcs;
            reqDcs = new OneWayDataContractSerializer("OneWay", "http://schemas.example.org/SimpleService");

            // Build soap request message
            byte[] soapBuffer = SoapMessageBuilder.BuildSoapMessage(header, reqDcs, req);

            // Send service request
            DpwsHttpClient httpClient;
            httpClient = new DpwsHttpClient();
            DpwsSoapResponse response;
            response = httpClient.SendRequest(soapBuffer, ServiceEndpoint, true, false);
        }
        
        public virtual TwoWayResponse TwoWayRequest(TwoWayRequest req)
        {

            // Create request header
            String action;
            action = "http://schemas.example.org/SimpleService/TwoWayRequest";
            WsWsaHeader header;
            header = new WsWsaHeader(action, null, ServiceEndpoint, null, EndpointAddress, null);

            // Create request serializer
            TwoWayRequestDataContractSerializer reqDcs;
            reqDcs = new TwoWayRequestDataContractSerializer("TwoWayRequest", "http://schemas.example.org/SimpleService");

            // Build soap request message
            byte[] soapBuffer = SoapMessageBuilder.BuildSoapMessage(header, reqDcs, req);

            // Send service request
            DpwsHttpClient httpClient;
            httpClient = new DpwsHttpClient();
            DpwsSoapResponse response;
            response = httpClient.SendRequest(soapBuffer, ServiceEndpoint, false, false);

            // Process response
            TwoWayResponseDataContractSerializer respDcs;
            respDcs = new TwoWayResponseDataContractSerializer("TwoWayResponse", "http://schemas.example.org/SimpleService");
            TwoWayResponse resp;
            resp = ((TwoWayResponse)(respDcs.ReadObject(response.Reader)));
            return resp;
        }
        
        public virtual TypeCheckResponse TypeCheckRequest(TypeCheckRequest req)
        {

            // Create request header
            String action;
            action = "http://schemas.example.org/SimpleService/TypeCheckRequest";
            WsWsaHeader header;
            header = new WsWsaHeader(action, null, ServiceEndpoint, null, EndpointAddress, null);

            // Create request serializer
            TypeCheckRequestDataContractSerializer reqDcs;
            reqDcs = new TypeCheckRequestDataContractSerializer("TypeCheckRequest", "http://schemas.example.org/SimpleService");

            // Build soap request message
            byte[] soapBuffer = SoapMessageBuilder.BuildSoapMessage(header, reqDcs, req);

            // Send service request
            DpwsHttpClient httpClient;
            httpClient = new DpwsHttpClient();
            DpwsSoapResponse response;
            response = httpClient.SendRequest(soapBuffer, ServiceEndpoint, false, false);

            // Process response
            TypeCheckResponseDataContractSerializer respDcs;
            respDcs = new TypeCheckResponseDataContractSerializer("TypeCheckResponse", "http://schemas.example.org/SimpleService");
            TypeCheckResponse resp;
            resp = ((TypeCheckResponse)(respDcs.ReadObject(response.Reader)));
            return resp;
        }
        
        public virtual AnyCheckResponse AnyCheckRequest(AnyCheckRequest req)
        {

            // Create request header
            String action;
            action = "http://schemas.example.org/SimpleService/AnyCheckRequest";
            WsWsaHeader header;
            header = new WsWsaHeader(action, null, ServiceEndpoint, null, EndpointAddress, null);

            // Create request serializer
            AnyCheckRequestDataContractSerializer reqDcs;
            reqDcs = new AnyCheckRequestDataContractSerializer("AnyCheckRequest", "http://schemas.example.org/SimpleService");

            // Build soap request message
            byte[] soapBuffer = SoapMessageBuilder.BuildSoapMessage(header, reqDcs, req);

            // Send service request
            DpwsHttpClient httpClient;
            httpClient = new DpwsHttpClient();
            DpwsSoapResponse response;
            response = httpClient.SendRequest(soapBuffer, ServiceEndpoint, false, false);

            // Process response
            AnyCheckResponseDataContractSerializer respDcs;
            respDcs = new AnyCheckResponseDataContractSerializer("AnyCheckResponse", "http://schemas.example.org/SimpleService");
            AnyCheckResponse resp;
            resp = ((AnyCheckResponse)(respDcs.ReadObject(response.Reader)));
            return resp;
        }
    }
}
