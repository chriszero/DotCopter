using Microsoft.SPOT;
using Microsoft.SPOT.IO;

namespace VolumeInfoSample
{
    public class Program
    {
        public static void Main()
        {
            Debug.Print("Volume Infos:");
            VolumeInfo[] volumeInfos = VolumeInfo.GetVolumes();
            foreach(VolumeInfo volumeInfo in volumeInfos)
            {
                PrintVolumeInfo(volumeInfo);
            }

            Debug.Print("File Systems:");
            string[] fileSystems = VolumeInfo.GetFileSystems();
            foreach (string fileSystem in fileSystems)
                Debug.Print(" Name=" + fileSystem);
        }

        private static void PrintVolumeInfo(VolumeInfo volumeInfo)
        {
            Debug.Print(" Name=" + volumeInfo.Name);
            Debug.Print(" Volume ID=" + volumeInfo.VolumeID);
            Debug.Print(" Volume Label=" + volumeInfo.VolumeLabel);
            Debug.Print(" File System=" + volumeInfo.FileSystem);
            Debug.Print(" Root Directory=" + volumeInfo.RootDirectory);
            Debug.Print(" Serial Number=" + volumeInfo.SerialNumber);
            Debug.Print(" Is Formatted=" + volumeInfo.IsFormatted);
            Debug.Print(" Total Size=" + volumeInfo.TotalSize);
            Debug.Print(" Total Free Space=" + volumeInfo.TotalFreeSpace);
            Debug.Print(string.Empty);
        }
    }
}
