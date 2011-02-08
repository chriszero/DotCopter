using System;
using System.IO;

namespace DotCopter.Commons.Serialization
{
    public class TelemetryFormatter:INETMFBinaryFormatter
    {

        public void Serialize(Stream stream, object graph)
        {
            throw new NotImplementedException();
        }

        public INETMFSerializable[] Deserialize(Stream stream)
        {
            INETMFSerializable[] items = new INETMFSerializable[stream.Length/44];
            int counter = 0;
            while (stream.Position < stream.Length)
            {
                byte[] buffer = new byte[44];
                stream.Read(buffer, 0, 44);
                INETMFSerializable item = new TelemetryData();
                item.Deserialize(buffer);
                items[counter] = item;
                counter++;
            }
            return items;
        }

        public INETMFSerializable DeserializeItem(Stream stream)
        {
            byte[] buffer = new byte[44];
            stream.Read(buffer, 0, 44);
            return (INETMFSerializable)(new TelemetryData().Deserialize(buffer));
        }

        public void Serialize(Stream stream, INETMFSerializable graph)
        {
            byte[] buffer = graph.Serialize();
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}
