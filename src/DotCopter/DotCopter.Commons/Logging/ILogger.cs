using DotCopter.Commons.Serialization;

namespace DotCopter.Commons.Logging
{
    public interface ILogger
    {
        void Flush();
        //void Write(byte[] buffer);
        //void Write(string entry);
        //void WriteTimestamp(long timestamp);
        void Write(INETMFSerializable obj);
    }
}
