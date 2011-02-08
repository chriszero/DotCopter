using System;
using System.Collections;
using Microsoft.SPOT;

namespace BinarySerializationSample2
{
    public enum MyEnum { A, B, C };

    public interface IMyInterface
    {
        void SomeMethod();
    }

    [Serializable]
    public class MyClass : IMyInterface
    {
        public byte b = 0xAA;

        #region IMyInterface Members
        public void SomeMethod()
        {
        }
        #endregion
    }

    [Serializable]
    public sealed class MySealedClass
    {
        public byte b = 0xAA;
    }

    [Serializable]
    public struct MyStruct
    {
        public int a;
    }

    [Serializable]
    [SerializationHints(Flags = SerializationFlags.DemandTrusted)]
    public class MySerializableClass2
    {
        [SerializationHints(BitPacked = 1)]
        public readonly bool a;
        [SerializationHints(BitPacked = 3)]
        private byte b; //for values from 0 - 7
        [SerializationHints(BitPacked = 2)]
        private MyEnum c; //only 3 members, so 2 bits are enough
        [SerializationHints(BitPacked = 4, RangeBias = 10)]
        public uint d = 25; //for values 10 - 25
        [SerializationHints(BitPacked = 4, RangeBias = 10, Scale = 2)]
        public uint e = 40; //for even values from 10 - 40 
        public string f;
        private float g;
        [SerializationHints(BitPacked = 56, RangeBias = 0x8C8FB4EEA270000)]
        private DateTime h = DateTime.Now; //from 1. Jan. 2007 to 5. May 2235 are 7 instead of 8 Bytes required
        [SerializationHints(Flags = SerializationFlags.PointerNeverNull | SerializationFlags.FixedType)]
        private MyClass i = new MyClass();
        [SerializationHints(Flags = SerializationFlags.PointerNeverNull)]
        private MySealedClass j = new MySealedClass();
        private IMyInterface k; //a type id needs to be stored
        public MyStruct l;
        [SerializationHints(ArraySize = 10, Flags = SerializationFlags.PointerNeverNull)]
        public byte[] m = new byte[10];
        [SerializationHints(BitPacked = 4, Flags = SerializationFlags.PointerNeverNull)]
        public byte[] n = new byte[5]; //for a 4 bit array size, 0 - 15 elements are possible
        [SerializationHints(Flags = SerializationFlags.PointerNeverNull)]
        public ArrayList o = new ArrayList();
    }
}
