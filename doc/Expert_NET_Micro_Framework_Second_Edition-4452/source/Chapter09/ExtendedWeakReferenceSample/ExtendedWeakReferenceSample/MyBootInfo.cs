using System;
using Microsoft.SPOT;

namespace ExtendedWeakReferenceSample
{
    //the class to be stored into flash memory, the class must be serializable
    [Serializable]
    internal sealed class MyBootInfo
    {
        private int bootCount;

        public MyBootInfo(int bootCount)
        {
            this.bootCount = bootCount;
        }

        public int BootCount
        {
            get { return this.bootCount; }
        }
    }
}
