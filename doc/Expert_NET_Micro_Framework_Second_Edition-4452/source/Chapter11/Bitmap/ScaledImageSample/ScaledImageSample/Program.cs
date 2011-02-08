using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation.Media;

namespace ScaledImageSample
{
    public class Program
    {
        public static void Main()
        {
            int screenWidth, screenHeight, bitsPerPixel, orientationDeg;
            HardwareProvider.HwProvider.GetLCDMetrics(out screenWidth, out screenHeight,
                                                      out bitsPerPixel, out orientationDeg);
            Bitmap bmp = new Bitmap(screenWidth, screenHeight);
            Bitmap soccerBall = Resources.GetBitmap(Resources.BitmapResources.SoccerBall);
            bmp.StretchImage(100, 50,            // destination coordinates
                          soccerBall,            // source image
                          soccerBall.Width / 2,  // half width
                          soccerBall.Height * 2, // double height
                          Bitmap.OpacityOpaque); // opacity
            bmp.Flush();
            Thread.Sleep(-1); //do not terminate app to see result
        }
    }
}
