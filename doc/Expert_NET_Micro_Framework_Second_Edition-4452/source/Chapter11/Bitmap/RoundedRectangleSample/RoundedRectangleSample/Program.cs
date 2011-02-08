using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation.Media;

namespace RoundedRectangleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            int screenWidth, screenHeight, bitsPerPixel, orientationDeg;
            HardwareProvider.HwProvider.GetLCDMetrics(out screenWidth, out screenHeight,
                                                      out bitsPerPixel, out orientationDeg);
            Bitmap bmp = new Bitmap(screenWidth, screenHeight);
            bmp.DrawRectangle(Color.White,           // outline color
                              1,                     // outline thickness
                              100, 100,              // x and y of top left corner
                              200, 100,              // width and height
                              10, 30,                // x and y corner radius
                              Color.White,           // gradient start color
                              0, 0,                  // gradient start coordinate  
                              Color.White,           // gradient end color
                              0, 0,                  // gradient end coordinate
                              Bitmap.OpacityOpaque); // opacity
            bmp.Flush();
            Thread.Sleep(-1); //do not terminate app to see result
        }
    }
}
