using System;
using System.Xml;
using System.Ext;
using System.Ext.Xml;
using Ws.ServiceModel;
using Ws.Services.Mtom;
using Ws.Services.Serialization;
using XmlElement = Ws.Services.Xml.WsXmlNode;
using XmlAttribute = Ws.Services.Xml.WsXmlAttribute;
using XmlConvert = Ws.Services.Serialization.WsXmlConvert;

namespace schemas.example.org.SimpleService
{
    
    
    [DataContract(Namespace="http://schemas.example.org/SimpleService")]
    public class OneWay
    {
        
        [DataMember(Order=0)]
        public int Param;
        
        [DataMember(IsNillable=true, IsRequired=false)]
        public XmlElement[] Any;
        
        [DataMember(IsNillable=true, IsRequired=false)]
        public XmlAttribute[] AnyAttr;
    }
    
    public class OneWayDataContractSerializer : DataContractSerializer
    {
        
        public OneWayDataContractSerializer(string _rootName, string _rootNameSpace) : 
                base(_rootName, _rootNameSpace)
        {
        }
        
        public override object ReadObject(XmlReader reader)
        {
            OneWay OneWayField = null;
            if (IsParentStartElement(reader, false, true))
            {
                OneWayField = new OneWay();
                OneWayField.AnyAttr = ReadAnyAttribute(reader);
                reader.Read();
                if (IsChildStartElement(reader, "Param", false, true))
                {
                    reader.Read();
                    OneWayField.Param = XmlConvert.ToInt32(reader.ReadString());
                    reader.ReadEndElement();
                }
                OneWayField.Any = ReadAnyElement(reader, false);
                reader.ReadEndElement();
            }
            return OneWayField;
        }
        
        public override void WriteObject(XmlWriter writer, object graph)
        {
            OneWay OneWayField = ((OneWay)(graph));
            if (WriteParentElement(writer, true, true, graph))
            {
                WriteAnyAttribute(writer, OneWayField.AnyAttr);
                if (WriteChildElement(writer, "Param", false, true, OneWayField.Param))
                {
                    writer.WriteString(XmlConvert.ToString(OneWayField.Param));
                    writer.WriteEndElement();
                }
                WriteAnyElement(writer, OneWayField.Any, false);
                writer.WriteEndElement();
            }
            return;
        }
    }
    
    [DataContract(Namespace="http://schemas.example.org/SimpleService")]
    public class TwoWayRequest
    {
        
        [DataMember(Order=0)]
        public int X;
        
        [DataMember(Order=1)]
        public int Y;
        
        [DataMember(IsNillable=true, IsRequired=false)]
        public XmlElement[] Any;
        
        [DataMember(IsNillable=true, IsRequired=false)]
        public XmlAttribute[] AnyAttr;
    }
    
    public class TwoWayRequestDataContractSerializer : DataContractSerializer
    {
        
        public TwoWayRequestDataContractSerializer(string _rootName, string _rootNameSpace) : 
                base(_rootName, _rootNameSpace)
        {
        }
        
        public override object ReadObject(XmlReader reader)
        {
            TwoWayRequest TwoWayRequestField = null;
            if (IsParentStartElement(reader, false, true))
            {
                TwoWayRequestField = new TwoWayRequest();
                TwoWayRequestField.AnyAttr = ReadAnyAttribute(reader);
                reader.Read();
                if (IsChildStartElement(reader, "X", false, true))
                {
                    reader.Read();
                    TwoWayRequestField.X = XmlConvert.ToInt32(reader.ReadString());
                    reader.ReadEndElement();
                }
                if (IsChildStartElement(reader, "Y", false, true))
                {
                    reader.Read();
                    TwoWayRequestField.Y = XmlConvert.ToInt32(reader.ReadString());
                    reader.ReadEndElement();
                }
                TwoWayRequestField.Any = ReadAnyElement(reader, false);
                reader.ReadEndElement();
            }
            return TwoWayRequestField;
        }
        
        public override void WriteObject(XmlWriter writer, object graph)
        {
            TwoWayRequest TwoWayRequestField = ((TwoWayRequest)(graph));
            if (WriteParentElement(writer, true, true, graph))
            {
                WriteAnyAttribute(writer, TwoWayRequestField.AnyAttr);
                if (WriteChildElement(writer, "X", false, true, TwoWayRequestField.X))
                {
                    writer.WriteString(XmlConvert.ToString(TwoWayRequestField.X));
                    writer.WriteEndElement();
                }
                if (WriteChildElement(writer, "Y", false, true, TwoWayRequestField.Y))
                {
                    writer.WriteString(XmlConvert.ToString(TwoWayRequestField.Y));
                    writer.WriteEndElement();
                }
                WriteAnyElement(writer, TwoWayRequestField.Any, false);
                writer.WriteEndElement();
            }
            return;
        }
    }
    
    [DataContract(Namespace="http://schemas.example.org/SimpleService")]
    public class TwoWayResponse
    {
        
        [DataMember(Order=0)]
        public int Sum;
        
        [DataMember(IsNillable=true, IsRequired=false)]
        public XmlElement[] Any;
        
        [DataMember(IsNillable=true, IsRequired=false)]
        public XmlAttribute[] AnyAttr;
    }
    
    public class TwoWayResponseDataContractSerializer : DataContractSerializer
    {
        
        public TwoWayResponseDataContractSerializer(string _rootName, string _rootNameSpace) : 
                base(_rootName, _rootNameSpace)
        {
        }
        
        public override object ReadObject(XmlReader reader)
        {
            TwoWayResponse TwoWayResponseField = null;
            if (IsParentStartElement(reader, false, true))
            {
                TwoWayResponseField = new TwoWayResponse();
                TwoWayResponseField.AnyAttr = ReadAnyAttribute(reader);
                reader.Read();
                if (IsChildStartElement(reader, "Sum", false, true))
                {
                    reader.Read();
                    TwoWayResponseField.Sum = XmlConvert.ToInt32(reader.ReadString());
                    reader.ReadEndElement();
                }
                TwoWayResponseField.Any = ReadAnyElement(reader, false);
                reader.ReadEndElement();
            }
            return TwoWayResponseField;
        }
        
        public override void WriteObject(XmlWriter writer, object graph)
        {
            TwoWayResponse TwoWayResponseField = ((TwoWayResponse)(graph));
            if (WriteParentElement(writer, true, true, graph))
            {
                WriteAnyAttribute(writer, TwoWayResponseField.AnyAttr);
                if (WriteChildElement(writer, "Sum", false, true, TwoWayResponseField.Sum))
                {
                    writer.WriteString(XmlConvert.ToString(TwoWayResponseField.Sum));
                    writer.WriteEndElement();
                }
                WriteAnyElement(writer, TwoWayResponseField.Any, false);
                writer.WriteEndElement();
            }
            return;
        }
    }
    
    [DataContract(Namespace="http://schemas.example.org/SimpleService")]
    public class TypeCheckRequest
    {
        
        [DataMember(Order=0)]
        public bool BoolCheck;
        
        [DataMember(Order=1)]
        public string DecimalCheck;
        
        [DataMember(Order=2)]
        public float FloatCheck;
        
        [DataMember(Order=3)]
        public string[] UriListCheck;
        
        [DataMember(IsNillable=true, IsRequired=false)]
        public XmlElement[] Any;
        
        [DataMember(IsNillable=true, IsRequired=false)]
        public XmlAttribute[] AnyAttr;
    }
    
    public class TypeCheckRequestDataContractSerializer : DataContractSerializer
    {
        
        public TypeCheckRequestDataContractSerializer(string _rootName, string _rootNameSpace) : 
                base(_rootName, _rootNameSpace)
        {
        }
        
        public override object ReadObject(XmlReader reader)
        {
            TypeCheckRequest TypeCheckRequestField = null;
            if (IsParentStartElement(reader, false, true))
            {
                TypeCheckRequestField = new TypeCheckRequest();
                TypeCheckRequestField.AnyAttr = ReadAnyAttribute(reader);
                reader.Read();
                if (IsChildStartElement(reader, "BoolCheck", false, true))
                {
                    reader.Read();
                    TypeCheckRequestField.BoolCheck = XmlConvert.ToBoolean(reader.ReadString());
                    reader.ReadEndElement();
                }
                if (IsChildStartElement(reader, "DecimalCheck", false, true))
                {
                    reader.Read();
                    TypeCheckRequestField.DecimalCheck = reader.ReadString();
                    reader.ReadEndElement();
                }
                if (IsChildStartElement(reader, "FloatCheck", false, true))
                {
                    reader.Read();
                    TypeCheckRequestField.FloatCheck = XmlConvert.ToSingle(reader.ReadString());
                    reader.ReadEndElement();
                }
                if (IsChildStartElement(reader, "UriListCheck", false, true))
                {
                    reader.Read();
                    string[] tempList = reader.ReadString().Split();
                    TypeCheckRequestField.UriListCheck = new string[tempList.Length];
                    int i;
                    for (i = 0; (i < tempList.Length); i = (i + 1))
                    {
                        TypeCheckRequestField.UriListCheck[i] = tempList[i];
                    }
                    reader.ReadEndElement();
                }
                TypeCheckRequestField.Any = ReadAnyElement(reader, false);
                reader.ReadEndElement();
            }
            return TypeCheckRequestField;
        }
        
        public override void WriteObject(XmlWriter writer, object graph)
        {
            TypeCheckRequest TypeCheckRequestField = ((TypeCheckRequest)(graph));
            if (WriteParentElement(writer, true, true, graph))
            {
                WriteAnyAttribute(writer, TypeCheckRequestField.AnyAttr);
                if (WriteChildElement(writer, "BoolCheck", false, true, TypeCheckRequestField.BoolCheck))
                {
                    writer.WriteString(XmlConvert.ToString(TypeCheckRequestField.BoolCheck));
                    writer.WriteEndElement();
                }
                if (WriteChildElement(writer, "DecimalCheck", false, true, TypeCheckRequestField.DecimalCheck))
                {
                    writer.WriteString(TypeCheckRequestField.DecimalCheck);
                    writer.WriteEndElement();
                }
                if (WriteChildElement(writer, "FloatCheck", false, true, TypeCheckRequestField.FloatCheck))
                {
                    writer.WriteString(XmlConvert.ToString(TypeCheckRequestField.FloatCheck));
                    writer.WriteEndElement();
                }
                if (WriteChildElement(writer, "UriListCheck", false, true, TypeCheckRequestField.UriListCheck))
                {
                    string tempList = "";
                    int i;
                    for (i = 0; (i < TypeCheckRequestField.UriListCheck.Length); i = (i + 1))
                    {
                        tempList = (tempList + TypeCheckRequestField.UriListCheck[i]);
                        if ((i 
                                    < (TypeCheckRequestField.UriListCheck.Length - 1)))
                        {
                            tempList = (tempList + " ");
                        }
                    }
                    writer.WriteString(tempList);
                    writer.WriteEndElement();
                }
                WriteAnyElement(writer, TypeCheckRequestField.Any, false);
                writer.WriteEndElement();
            }
            return;
        }
    }
    
    [DataContract(Namespace="http://schemas.example.org/SimpleService")]
    public class TypeCheckResponse
    {
        
        [DataMember(Order=0)]
        public bool BoolCheck;
        
        [DataMember(Order=1)]
        public string DecimalCheck;
        
        [DataMember(Order=2)]
        public float FloatCheck;
        
        [DataMember(Order=3)]
        public string[] UriListCheck;
        
        [DataMember(IsNillable=true, IsRequired=false)]
        public XmlElement[] Any;
        
        [DataMember(IsNillable=true, IsRequired=false)]
        public XmlAttribute[] AnyAttr;
    }
    
    public class TypeCheckResponseDataContractSerializer : DataContractSerializer
    {
        
        public TypeCheckResponseDataContractSerializer(string _rootName, string _rootNameSpace) : 
                base(_rootName, _rootNameSpace)
        {
        }
        
        public override object ReadObject(XmlReader reader)
        {
            TypeCheckResponse TypeCheckResponseField = null;
            if (IsParentStartElement(reader, false, true))
            {
                TypeCheckResponseField = new TypeCheckResponse();
                TypeCheckResponseField.AnyAttr = ReadAnyAttribute(reader);
                reader.Read();
                if (IsChildStartElement(reader, "BoolCheck", false, true))
                {
                    reader.Read();
                    TypeCheckResponseField.BoolCheck = XmlConvert.ToBoolean(reader.ReadString());
                    reader.ReadEndElement();
                }
                if (IsChildStartElement(reader, "DecimalCheck", false, true))
                {
                    reader.Read();
                    TypeCheckResponseField.DecimalCheck = reader.ReadString();
                    reader.ReadEndElement();
                }
                if (IsChildStartElement(reader, "FloatCheck", false, true))
                {
                    reader.Read();
                    TypeCheckResponseField.FloatCheck = XmlConvert.ToSingle(reader.ReadString());
                    reader.ReadEndElement();
                }
                if (IsChildStartElement(reader, "UriListCheck", false, true))
                {
                    reader.Read();
                    string[] tempList = reader.ReadString().Split();
                    TypeCheckResponseField.UriListCheck = new string[tempList.Length];
                    int i;
                    for (i = 0; (i < tempList.Length); i = (i + 1))
                    {
                        TypeCheckResponseField.UriListCheck[i] = tempList[i];
                    }
                    reader.ReadEndElement();
                }
                TypeCheckResponseField.Any = ReadAnyElement(reader, false);
                reader.ReadEndElement();
            }
            return TypeCheckResponseField;
        }
        
        public override void WriteObject(XmlWriter writer, object graph)
        {
            TypeCheckResponse TypeCheckResponseField = ((TypeCheckResponse)(graph));
            if (WriteParentElement(writer, true, true, graph))
            {
                WriteAnyAttribute(writer, TypeCheckResponseField.AnyAttr);
                if (WriteChildElement(writer, "BoolCheck", false, true, TypeCheckResponseField.BoolCheck))
                {
                    writer.WriteString(XmlConvert.ToString(TypeCheckResponseField.BoolCheck));
                    writer.WriteEndElement();
                }
                if (WriteChildElement(writer, "DecimalCheck", false, true, TypeCheckResponseField.DecimalCheck))
                {
                    writer.WriteString(TypeCheckResponseField.DecimalCheck);
                    writer.WriteEndElement();
                }
                if (WriteChildElement(writer, "FloatCheck", false, true, TypeCheckResponseField.FloatCheck))
                {
                    writer.WriteString(XmlConvert.ToString(TypeCheckResponseField.FloatCheck));
                    writer.WriteEndElement();
                }
                if (WriteChildElement(writer, "UriListCheck", false, true, TypeCheckResponseField.UriListCheck))
                {
                    string tempList = "";
                    int i;
                    for (i = 0; (i < TypeCheckResponseField.UriListCheck.Length); i = (i + 1))
                    {
                        tempList = (tempList + TypeCheckResponseField.UriListCheck[i]);
                        if ((i 
                                    < (TypeCheckResponseField.UriListCheck.Length - 1)))
                        {
                            tempList = (tempList + " ");
                        }
                    }
                    writer.WriteString(tempList);
                    writer.WriteEndElement();
                }
                WriteAnyElement(writer, TypeCheckResponseField.Any, false);
                writer.WriteEndElement();
            }
            return;
        }
    }
    
    [DataContract(Namespace="http://schemas.example.org/SimpleService")]
    public class AnyCheckRequest
    {
        
        [DataMember(IsNillable=true, IsRequired=false)]
        public XmlElement[] Any;
        
        [DataMember(IsNillable=true, IsRequired=false)]
        public XmlAttribute[] AnyAttr;
    }
    
    public class AnyCheckRequestDataContractSerializer : DataContractSerializer
    {
        
        public AnyCheckRequestDataContractSerializer(string _rootName, string _rootNameSpace) : 
                base(_rootName, _rootNameSpace)
        {
        }
        
        public override object ReadObject(XmlReader reader)
        {
            AnyCheckRequest AnyCheckRequestField = null;
            if (IsParentStartElement(reader, false, true))
            {
                AnyCheckRequestField = new AnyCheckRequest();
                AnyCheckRequestField.AnyAttr = ReadAnyAttribute(reader);
                reader.Read();
                AnyCheckRequestField.Any = ReadAnyElement(reader, false);
                reader.ReadEndElement();
            }
            return AnyCheckRequestField;
        }
        
        public override void WriteObject(XmlWriter writer, object graph)
        {
            AnyCheckRequest AnyCheckRequestField = ((AnyCheckRequest)(graph));
            if (WriteParentElement(writer, true, true, graph))
            {
                WriteAnyAttribute(writer, AnyCheckRequestField.AnyAttr);
                WriteAnyElement(writer, AnyCheckRequestField.Any, false);
                writer.WriteEndElement();
            }
            return;
        }
    }
    
    [DataContract(Namespace="http://schemas.example.org/SimpleService")]
    public class AnyCheckResponse
    {
        
        [DataMember(IsNillable=true, IsRequired=false)]
        public XmlElement[] Any;
        
        [DataMember(IsNillable=true, IsRequired=false)]
        public XmlAttribute[] AnyAttr;
    }
    
    public class AnyCheckResponseDataContractSerializer : DataContractSerializer
    {
        
        public AnyCheckResponseDataContractSerializer(string _rootName, string _rootNameSpace) : 
                base(_rootName, _rootNameSpace)
        {
        }
        
        public override object ReadObject(XmlReader reader)
        {
            AnyCheckResponse AnyCheckResponseField = null;
            if (IsParentStartElement(reader, false, true))
            {
                AnyCheckResponseField = new AnyCheckResponse();
                AnyCheckResponseField.AnyAttr = ReadAnyAttribute(reader);
                reader.Read();
                AnyCheckResponseField.Any = ReadAnyElement(reader, false);
                reader.ReadEndElement();
            }
            return AnyCheckResponseField;
        }
        
        public override void WriteObject(XmlWriter writer, object graph)
        {
            AnyCheckResponse AnyCheckResponseField = ((AnyCheckResponse)(graph));
            if (WriteParentElement(writer, true, true, graph))
            {
                WriteAnyAttribute(writer, AnyCheckResponseField.AnyAttr);
                WriteAnyElement(writer, AnyCheckResponseField.Any, false);
                writer.WriteEndElement();
            }
            return;
        }
    }
    
    [ServiceContract(Namespace="http://schemas.example.org/SimpleService")]
    public interface ISimpleService
    {
        
        [OperationContract(Action="http://schemas.example.org/SimpleService/OneWay", IsOneWay=true)]
        void OneWay(OneWay req);
        
        [OperationContract(Action="http://schemas.example.org/SimpleService/TwoWayRequest")]
        TwoWayResponse TwoWay(TwoWayRequest req);
        
        [OperationContract(Action="http://schemas.example.org/SimpleService/TypeCheckRequest")]
        TypeCheckResponse TypeCheck(TypeCheckRequest req);
        
        [OperationContract(Action="http://schemas.example.org/SimpleService/AnyCheckRequest")]
        AnyCheckResponse AnyCheck(AnyCheckRequest req);
    }
}
