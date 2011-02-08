using System.IO;
using DotCopter.Commons.Serialization;

namespace DotCopter.Commons.Logging
{
    public class PersistanceReader
    {
        private readonly INETMFBinaryFormatter _netmfBinaryFormatter;

        public PersistanceReader(INETMFBinaryFormatter netmfBinaryFormatter)
        {
            _netmfBinaryFormatter = netmfBinaryFormatter;
            
        }
        public INETMFSerializable[] GetMessages(string readPath)
        {
            FileStream readFile = new FileStream(readPath, FileMode.Open, FileAccess.Read);
            return _netmfBinaryFormatter.Deserialize(readFile);
        }

        public INETMFSerializable GetMessage(FileStream readFile)
        {
            return _netmfBinaryFormatter.DeserializeItem(readFile);
        }
    }
}
