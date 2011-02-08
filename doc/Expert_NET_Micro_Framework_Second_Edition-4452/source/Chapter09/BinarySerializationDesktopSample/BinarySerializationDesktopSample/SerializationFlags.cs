using System;

namespace Microsoft.SPOT
{
    [Flags]
    public enum SerializationFlags
    {
        //Encrypted = 1,
        //Compressed = 2,
        //Optional = 4,
        PointerNeverNull = 0x10,
        //ElementsNeverNull = 0x20,
        FixedType = 0x100,
        //DemandTrusted = 0x10000
    }
}
