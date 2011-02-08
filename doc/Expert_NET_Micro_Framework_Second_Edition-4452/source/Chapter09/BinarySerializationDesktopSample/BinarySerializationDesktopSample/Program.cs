using System;
using Microsoft.SPOT;

namespace BinarySerializationDesktopSample
{
    class Program
    {
        static void Main(string[] args)
        {
            MySerializableClass original = new MySerializableClass(1, "ABCD", 3, MyEnum.B, 0.1f);
            Console.WriteLine("original: " + original.ToString());
            byte[] buffer = Reflection.Serialize(original, typeof(MySerializableClass));
            MySerializableClass restored = (MySerializableClass)Reflection.Deserialize(buffer, typeof(MySerializableClass));
            Console.WriteLine("restored: " + restored.ToString());
            Console.WriteLine("Number of bytes: " + buffer.Length.ToString());
            DumpBuffer(buffer);
            Console.ReadKey(false);
        }

        private static void DumpBuffer(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            for (int i = 0; i < buffer.Length; ++i)
            {
                if (i > 0 && i % 16 == 0)
                    Console.WriteLine("");
                Console.Write(buffer[i].ToString("X2") + " ");
            }
            Console.WriteLine("");
        }
    }
}
