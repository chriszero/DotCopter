using System;

namespace DotCopter.Commons.Utilities
{
    //http://www.rmfm.org/dual_rates_and_exponential1.htm
    public class Scale
    {
        private readonly float[] _coefficients;
        private readonly float _offset;
        private readonly int _length;

        public Scale(float offset, params float[] coefficients)
        {
            _coefficients = coefficients;
            _length = coefficients.Length;
            _offset = offset;
        }

        public float Calculate(float value)
        {
            float output = 0;

            for (int i = 0; i < _length; i++)
            {
                output += (float)Math.Pow(value + _offset, i) * _coefficients[_length - i - 1];
            }
            return output;
        }
    }
}
