using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace DotCopter.Benchmark
{
    public class BitConverterTest
    {
        public void Run(int iterations)
        {

            byte[] buffer = new byte[8];
            long value = 42;

            long start = DateTime.Now.Ticks;
            for(long i=0;i<iterations;i++)
                PutManualBitConvert(ref buffer, ref value);
            Debug.Print("Manual Put: " + (DateTime.Now.Ticks - start));
            
            start = DateTime.Now.Ticks;
            for(long i=0;i<iterations;i++)
                GetManualBitConvert(ref value, ref buffer);
            Debug.Print("Manual Get: " + (DateTime.Now.Ticks - start));

            start = DateTime.Now.Ticks;
            for (int i = 0; i < iterations; i++)
                PutUtilityBitConvert(ref buffer, ref value);
            Debug.Print("Utility Put: " + (DateTime.Now.Ticks - start));

            start = DateTime.Now.Ticks;
            for (long i = 0; i < iterations; i++)
                GetUtilityBitConvert(ref value, ref buffer);
            Debug.Print("Utility Get: " + (DateTime.Now.Ticks - start));
        }

        private void PutManualBitConvert(ref byte[] buffer, ref long value)
        {
            buffer[0] = (byte) ((value >> 56) & 0xFF);
            buffer[1] = (byte) ((value >> 48) & 0xFF);
            buffer[2] = (byte) ((value >> 40) & 0xFF);
            buffer[3] = (byte) ((value >> 32) & 0xFF);
            buffer[4] = (byte) ((value >> 24) & 0xFF);
            buffer[5] = (byte) ((value >> 16) & 0xFF);
            buffer[6] = (byte) ((value >> 8) & 0xFF);
            buffer[7] = (byte) (value & 0xFF);
        }

        private void GetManualBitConvert(ref long value, ref byte[] buffer)
        {
            value = (long) buffer[0] << 56 |
                    (long) buffer[1] << 48 |
                    (long) buffer[2] << 40 |
                    (long) buffer[3] << 32 |
                    (long) buffer[4] << 32 |
                    (long) buffer[5] << 16 |
                    (long) buffer[6] << 8 |
                    (long) buffer[7];
        }

        private void PutUtilityBitConvert(ref byte[] buffer, ref long value)
        {
            Utility.InsertValueIntoArray(buffer, 0, 4, (uint)(value >> 32));
            Utility.InsertValueIntoArray(buffer, 4, 4, (uint)value);
        }

        private void GetUtilityBitConvert(ref long value, ref byte[] buffer)
        {
            value = (long)Utility.ExtractValueFromArray(buffer, 0, 4) << 32;
            value |= Utility.ExtractValueFromArray(buffer, 4, 4);
        }
    }
}
