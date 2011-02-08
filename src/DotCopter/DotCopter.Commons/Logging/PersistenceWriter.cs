using System.IO;
using DotCopter.Commons.Serialization;

namespace DotCopter.Commons.Logging
{
    public class PersistenceWriter : ILogger
    {
        private readonly INETMFBinaryFormatter _formatter;
        private readonly string _fileName;
        private readonly FileStream _fileStream;

        public PersistenceWriter(string fileName, INETMFBinaryFormatter formatter)
        {
            _formatter = formatter;
            _fileName = fileName;
            _fileStream = new FileStream(_fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 512);
        }

        public void Flush()
        {
            _fileStream.Flush();
        }


        public void Write(INETMFSerializable obj)
        {
            _formatter.Serialize(_fileStream, obj);

        }

        public void WriteLine(string text)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(text);
            _fileStream.Write(buffer, 0, buffer.Length);
        }
    }
}