using System.IO;
using Microsoft.SPOT;

namespace FileInputOutputOperationsSample
{
    public class Program
    {
        public static void Main()
        {
            // Select storage device
            // You need to change this when running not on the emulator
            Directory.SetCurrentDirectory(@"\WINFS");
            
            // Create a UTF8 text file
            using (StreamWriter w = new StreamWriter("File1.txt"))
            {
                w.WriteLine("Hello world!");
                w.WriteLine("This is a two line text file.");
            }

            // Create a directory
            Directory.CreateDirectory(@"Dir1");

            // Copy file from root to new sub dir
            // All paths are relative to current dir
            File.Copy(@"File1.txt",      // Source file
                      @"Dir1\File2.txt", // Dest file
                      true               // Overwrite if already exists?
                      ); 

            // Read and print new text file
            using (StreamReader r = new StreamReader(@"Dir1\File2.txt"))
            {
                if (r.BaseStream.Length > 0)
                {
                    do
                    {
                        string line = r.ReadLine();
                        Debug.Print(line);
                    } while (!r.EndOfStream);
                }
            }
        }
    }
}
