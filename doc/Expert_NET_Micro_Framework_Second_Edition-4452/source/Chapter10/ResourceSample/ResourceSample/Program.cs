using System;
using Microsoft.SPOT;

namespace ResourceSample
{
    public class Program
    {
        public static void Main()
        {
            Debug.Print(Resources.GetString(Resources.StringResources.HelloWorld));
            Debug.Print(Resources.GetString(Resources.StringResources.Copyright));

            Bitmap bmp = Resources.GetBitmap(Resources.BitmapResources.MFsnowflake);
            Font font = Resources.GetFont(Resources.FontResources.small);
            byte[] buffer = Resources.GetBytes(Resources.BinaryResources.MyPDF);
        }
    }
}
