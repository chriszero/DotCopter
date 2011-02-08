using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation.Media;

namespace TransparentImageSample
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
            //make background of the ball transparent
            //using the color of top left corner pixel
            soccerBall.MakeTransparent(soccerBall.GetPixel(0, 0));
            bmp.DrawImage(100, 50,               // destination coordinates
                          soccerBall,            // source image
                          0, 0,                  // source coordinates
                          soccerBall.Width,      // source width
                          soccerBall.Height,     // source height
                          Bitmap.OpacityOpaque); // opacity
            bmp.Flush();
            Thread.Sleep(-1); //do not terminate app to see result
        }
    }
}
