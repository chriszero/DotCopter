using DotCopter.Commons.Serialization;

namespace DotCopter.Commons.Logging
{
    public class NullLogger : ILogger
    {
        public void Flush(){}
        public void Write(byte[] buffer){}
        public void Write(string entry){}
        public void WriteTimestamp(long timestamp){}
        public void Write(INETMFSerializable obj) { }
    }
}
