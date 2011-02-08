using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation.Media;

namespace OpacityWithRectanglesSample
{
    class Program
    {
        static void Main(string[] args)
        {
            int screenWidth, screenHeight, bitsPerPixel, orientationDeg;
            HardwareProvider.HwProvider.GetLCDMetrics(out screenWidth, out screenHeight,
                                                      out bitsPerPixel, out orientationDeg);
            Bitmap bmp = new Bitmap(screenWidth, screenHeight);
            //drawing white background
            bmp.DrawRectangle(Color.White,           // outline color
                              0,                     // outline thickness
                              0, 0,                  // x and y of top left corner
                              bmp.Width, bmp.Height, // width and height
                              0, 0,                  // y and y corner radius
                              Color.White,           // gradient start color
                              0, 0,                  // gradient start coordinates  
                              Color.White,           // gradient end color
                              0, 0,                  // gradient end coordinates
                              Bitmap.OpacityOpaque); // reduced opacity

            Color[] colors = new Color[] {
                                   ColorUtility.ColorFromRGB(0xFF, 0, 0), // red
                                   ColorUtility.ColorFromRGB(0, 0xFF, 0), // green
                                   ColorUtility.ColorFromRGB(0, 0, 0xFF)  // blue
            };

            for (int i = 0; i < colors.Length; ++i)
            {
                Color color = colors[i];
                bmp.DrawRectangle(color,                    // outline color
                                  0,                        // outline thickness
                                  50 + i * 20, 50 + i * 20, // x and y of top left corner
                                  200, 100,                 // width and height
                                  0, 0,                     // x and y corner radius
                                  color,                    // gradient start color
                                  0, 0,                     // gradient start coordinates  
                                  color,                    // gradient end color
                                  0, 0,                     // gradient end coordinates
                                  64);                      // reduced opacity
            }

            bmp.Flush();
            Thread.Sleep(-1); //do not terminate app to see result
        }
    }
}
