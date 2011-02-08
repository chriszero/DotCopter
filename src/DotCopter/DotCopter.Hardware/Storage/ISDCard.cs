namespace DotCopter.Hardware.Storage
{
    public interface ISDCard
    {
        void MountFileSystem();
        void UnMountFileSystem();
        void MountMassStorage();
        void UnMountMassStorage();
        bool IsFileSystemMounted { get; }
        bool IsMassStorageMounted { get; }
    }
}
