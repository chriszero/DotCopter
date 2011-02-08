using System;
using System.IO;

namespace Microsoft.SPOT
{
    public static class Reflection
    {
        public static byte[] Serialize(object obj, Type type)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (type == null)
                throw new ArgumentNullException("type");
            if (!BinaryFormatter.IsTypeSerializable(type))
                return null;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter.SerializeObject(stream, obj, type);
                return stream.ToArray();
            }
        }

        public static object Deserialize(byte[] buffer, Type type)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if (type == null)
                throw new ArgumentNullException("type");
            using (MemoryStream stream = new MemoryStream(buffer))
                return BinaryFormatter.DeSerializeObject(stream, type);
        }
    }
}
