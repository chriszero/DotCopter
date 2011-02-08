using System;

namespace DotCopter.OptimizationTests
{
    public class GCTests
    {
        private double _num = 0;
        public double RunMathOperationWithGc()
        {
            for (int i = 1; i < 2; i++)
            {
                _num = _num * DateTime.Now.Ticks;
            }
            //Debug.GC(true);
            return _num;
        }
    }
}
