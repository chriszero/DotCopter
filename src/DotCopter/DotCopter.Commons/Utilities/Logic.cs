namespace DotCopter.Commons.Utilities
{
    public static class Logic
    {
        public static float Constrain(float value, float minValue, float maxValue)
        {
            return value < minValue ? minValue : (value > maxValue ? maxValue : value);
        }
        public static int Constrain(int value, int minValue, int maxValue)
        {
            return value < minValue ? minValue : (value > maxValue ? maxValue : value);
        }
    }
}
