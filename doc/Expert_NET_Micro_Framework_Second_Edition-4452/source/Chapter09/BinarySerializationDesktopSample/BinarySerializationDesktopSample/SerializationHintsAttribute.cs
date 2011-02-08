using System;

namespace Microsoft.SPOT
{
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class, Inherited = true, AllowMultiple=false)]
        public class SerializationHintsAttribute : Attribute
        {
            public int ArraySize;
            public int BitPacked;
            public SerializationFlags Flags;
            public long RangeBias;
            public ulong Scale;
        }
}
