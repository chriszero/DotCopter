using System.IO;
using System.Threading;
using DotCopter.Commons.Logging;
using DotCopter.Commons.Serialization;
using DotCopter.Hardware.Storage;
using Microsoft.SPOT.Hardware;

namespace DotCopter.Hardware.Implementations.Storage
{
    public class TelemetryPresenter
    {
        private readonly OutputPort _led;
        public TelemetryPresenter(ISDCard sdCard, Cpu.Pin pin)
        {
            _led = new OutputPort(pin, false);
            var reader = new PersistanceReader(new TelemetryFormatter());
            sdCard.MountFileSystem();
            var files = Directory.GetFiles("\\SD");
            foreach (var file in files)
            {
                ParseToCsv(file,reader);
                //File.Delete(file);
            }
            sdCard.MountMassStorage();
            while(true)
            {
                _led.Write(true);
                Thread.Sleep(1000);
                _led.Write(false);
                Thread.Sleep(1000);
            }
        }
        private void ParseToCsv(string path, PersistanceReader reader)
        {
            if (path.Substring(path.Length-3,3) != "bin")
                return;
            var newFileName = path.Substring(0, path.LastIndexOf('.')) + ".csv";
            var streamWriter = new StreamWriter(new FileStream(newFileName,FileMode.OpenOrCreate,FileAccess.Write));

            var dataStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            streamWriter.WriteLine("time,gP,gR,gY,rP,rR,rY,pP,pR,pY");
            //written like this to provide some feedback that the conversion is actually happening);
            while (true)
            {
                _led.Write(true);
                if (dataStream.Position > dataStream.Length - 44) break;
                streamWriter.WriteLine(reader.GetMessage(dataStream).ToString());
                _led.Write(false);
                if (dataStream.Position > dataStream.Length - 44) break;
                streamWriter.WriteLine(reader.GetMessage(dataStream).ToString());
            }

            dataStream.Close();
            streamWriter.Close();

            /* You will run out of memory doing it this way in the mcu but you should do it this way in the windows program
            var items = reader.GetMessages(path);
            foreach (var serializable in items)
            {
                var line = serializable.ToString();
                streamWriter.WriteLine(line);
            }*/


        }
    }

}
