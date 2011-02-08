using System;
using Microsoft.SPOT;

namespace Quad.Net.Commons.Logging
{
    public interface IRealTimeLogger
    {
        void Update(bool armingStatus, bool loggingCommand);
        void Write(byte[] buffer);
    }
}
