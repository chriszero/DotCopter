using Microsoft.SPOT.Hardware;

namespace DotCopter.Commons.Utilities
{
    public static class BitConverter
    {
        public static void ToBytes(ref byte[] buffer, int offset, long value)
        {
            Utility.InsertValueIntoArray(buffer, offset, 4, (uint)(value >> 32));
            Utility.InsertValueIntoArray(buffer, offset + 4, 4, (uint)value);
        }

        public static unsafe void ToBytes(ref byte[] buffer, int offset, float value)
        {
            Utility.InsertValueIntoArray(buffer, offset, 4, *((uint*)&value));
        }

        public static long ToLong(byte[] buffer, int offset)
        {
            long value = (long)Utility.ExtractValueFromArray(buffer, offset, 4) << 32;
            value |= Utility.ExtractValueFromArray(buffer, offset + 4, 4);
            return value;
        }

        public static unsafe float ToFloat(byte[] buffer, int offset)
        {
            uint value = Utility.ExtractValueFromArray(buffer, offset, 4);
            return *((float*)&value);
        }

        public static short ToShort(byte[] buffer, int offset)
        {
            return (short) Utility.ExtractValueFromArray(buffer, offset, 2);
        }
    }

}
