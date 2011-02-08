using System;
using DotCopter.Commons.Serialization;
using Microsoft.SPOT;

namespace DotCopter.Commons.Logging
{
    public class DebugLogger : ILogger
    {
        public void Flush()
        {
            throw new NotImplementedException();
        }

        public void Write(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public void Write(string entry)
        {
            Debug.Print(DateTime.Now.ToString("u")+":"+DateTime.Now.Millisecond.ToString()+ ":\t" + entry);
        }

        public void WriteTimestamp(long timestamp)
        {
            throw new NotImplementedException();
        }

        public void Write(INETMFSerializable obj)
        {
            throw new NotImplementedException();
        }
    }
}
