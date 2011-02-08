using System;
using Microsoft.SPOT;

namespace BinarySerializationSample
{
    public enum MyEnum : short { A, B, C };

    [Serializable]
    public class MySerializableClass
    {
        public int a;
        public string b;
        private byte c;
        private MyEnum d;
        private float e;
        private DateTime dt;

        public MySerializableClass(int a, string b, byte c, MyEnum d, float e)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.dt = this.dt = new DateTime(2007, 1, 22);
        }

        public override string ToString()
        {
            return "a=" + a.ToString() + ", b=" + b + ", c=" + c.ToString() + ", d=" + d.ToString() +
                   ", e=" + e.ToString("F2");
        }
    }
}
