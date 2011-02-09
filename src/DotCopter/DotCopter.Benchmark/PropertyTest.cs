using System;
using Microsoft.SPOT;

namespace DotCopter.Benchmark
{
    public class PropertyTest
    {
        private int _local;

        private int _localStore;
        private int StoredProperty { get { return _localStore; } set { _localStore = value; } }

        private int AutomaticProperty { get; set; }

        private int _methodStore;
        private int GetInt()
        {
            return _methodStore;
        }
        private void SetInt(int value)
        {
            _methodStore = value;
        }
        public unsafe void Run(int iterations)
        {
            long start = DateTime.Now.Ticks;
            for (int i = 0; i < iterations; i++)
                _local = _local + 1;
            Debug.Print("Local: " + (DateTime.Now.Ticks-start)/10);

            start = DateTime.Now.Ticks;
            for (int i = 0; i < iterations; i++)
                StoredProperty = StoredProperty + 1;
            Debug.Print("StoredProperty:" + (DateTime.Now.Ticks - start) / 10);

            start = DateTime.Now.Ticks;
            for (int i = 0; i < iterations; i++)
                AutomaticProperty = AutomaticProperty + 1;
            Debug.Print("AutomaticProperty:" + (DateTime.Now.Ticks - start) / 10);

            start = DateTime.Now.Ticks;
            for (int i = 0; i < iterations; i++)
                SetInt(GetInt() + 1);
            Debug.Print("Method:" + (DateTime.Now.Ticks - start) / 10);


            start = DateTime.Now.Ticks;
            for (int i = 0; i < iterations; i++)
                _local = _local + 1;
            Debug.Print("Local: " + (DateTime.Now.Ticks - start) / 10);

        }
    }
}
