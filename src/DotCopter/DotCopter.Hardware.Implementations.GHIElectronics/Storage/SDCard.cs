using DotCopter.Hardware.Storage;
using GHIElectronics.NETMF.IO;
using GHIElectronics.NETMF.USBClient;

namespace DotCopter.Hardware.Implementations.GHIElectronics.Storage
{
    public class SDCard : ISDCard
    {
        private USBC_MassStorage _massStorage;
        private readonly PersistentStorage _persistentStorage;
        
        public SDCard()
        {
            IsMassStorageMounted = false;
            IsFileSystemMounted = false;
            _persistentStorage = new PersistentStorage("SD");
        }

        public void MountFileSystem()
        {
            if(IsFileSystemMounted)
                return;
            if(IsMassStorageMounted)
                UnMountMassStorage();
            _persistentStorage.MountFileSystem();
            IsFileSystemMounted = true;
        }

        public void UnMountFileSystem()
        {
            if(!IsFileSystemMounted)
                return;
            _persistentStorage.UnmountFileSystem();
            IsFileSystemMounted = false;
        }



        public void MountMassStorage()
        {
            if(IsMassStorageMounted)
                return;
            if (IsFileSystemMounted)
                UnMountFileSystem();
            _massStorage = USBClientController.StandardDevices.StartMassStorage();
            _massStorage.AttachLun(0, _persistentStorage, " ", " ");
            _massStorage.EnableLun(0);
            IsMassStorageMounted = true;
        }

        public void UnMountMassStorage()
        {
            if(!IsMassStorageMounted)
                return;
            _massStorage.DisableLun(0);
            IsMassStorageMounted = false;
        }

        public bool IsFileSystemMounted { get; private set; }

        public bool IsMassStorageMounted { get; private set; }
    }
}
