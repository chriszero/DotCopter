using System;
using System.IO;

namespace DotCopter.Commons.Serialization
{
    public interface INETMFBinaryFormatter
    {
        void Serialize(Stream stream, Object graph);//not implemented
        INETMFSerializable[] Deserialize(Stream stream);
        INETMFSerializable DeserializeItem(Stream stream);
        void Serialize(Stream stream, INETMFSerializable graph);//optimistic
    }
}
