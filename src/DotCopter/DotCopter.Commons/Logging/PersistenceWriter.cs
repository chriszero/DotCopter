using System.IO;
using DotCopter.Commons.Serialization;

namespace DotCopter.Commons.Logging
{
    public class PersistenceWriter : ILogger
    {
        private readonly INETMFBinaryFormatter _formatter;
        private readonly Stream _stream;

        public PersistenceWriter(Stream stream, INETMFBinaryFormatter formatter)
        {
            _formatter = formatter;
            _stream = stream;
        }

        public void Flush()
        {
            _stream.Flush();
        }


        public void Write(INETMFSerializable obj)
        {
            _formatter.Serialize(_stream, obj);

        }

        public void WriteLine(string text)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(text);
            _stream.Write(buffer, 0, buffer.Length);
        }
    }
}