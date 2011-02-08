using System;
using System.IO;
using Microsoft.SPOT;

namespace Quad.Net.Commons.Logging
{
    public class TelemetryLogger : IRealTimeLogger
    {
        private FileStream _fileStream;
        private bool _log;
        private bool _armed;

        public TelemetryLogger(string path)
        {
            _fileStream = File.OpenWrite(path);
        }

        public void Update(bool armed, bool log)
        {
            if(!armed && !log) _fileStream.Flush();
            _armed = armed;
            _log = log;
        }

        public void Write(byte[] buffer)
        {
            if (_armed && _log)
                _fileStream.Write(buffer, 0, buffer.Length);
        }
    }
}
