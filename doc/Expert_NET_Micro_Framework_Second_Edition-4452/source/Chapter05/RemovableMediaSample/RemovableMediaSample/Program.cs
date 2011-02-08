using Microsoft.SPOT;
using Microsoft.SPOT.IO;
using System.Threading;

namespace RemovableMediaSample
{
    public class Program
    {
        public static void Main()
        {
            RemovableMedia.Insert += new InsertEventHandler(Media_Insert);
            RemovableMedia.Eject += new EjectEventHandler(Media_Eject);

            Thread.Sleep(Timeout.Infinite); // Do not terminate app
        }

        private static void Media_Insert(object sender, MediaEventArgs e)
        {
            Debug.Print("Media Insert:");
            PrintVolumeInfo(e.Volume);
        }

        private static void Media_Eject(object sender, MediaEventArgs e)
        {
            Debug.Print("Media Eject:");
            PrintVolumeInfo(e.Volume);
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
