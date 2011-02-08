using System;

namespace MyExtensions
{
    public static class MyStringExtensions
    {
        public static bool Contains(this string str, string value)
        {
            return str.IndexOf(value) > 0;
        }
    }
}
