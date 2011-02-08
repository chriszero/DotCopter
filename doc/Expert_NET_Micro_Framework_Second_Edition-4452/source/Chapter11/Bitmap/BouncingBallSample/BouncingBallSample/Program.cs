using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation.Media;

namespace BouncingBallSample
{
    public class Program
    {
        public static void Main()
        {
            int screenWidth, screenHeight, bitsPerPixel, orientationDeg;
            HardwareProvider.HwProvider.GetLCDMetrics(out screenWidth, out screenHeight,
                                                      out bitsPerPixel, out orientationDeg);
            //prepare background with a color gradient
            Bitmap backgroundImg = new Bitmap(screenWidth, screenHeight);
            backgroundImg.DrawRectangle(Color.White,           // outline color
                                        0,                     // outline thickness
                                        0, 0,                  // x and y of top left corner
                                        backgroundImg.Width,   // width
                                        backgroundImg.Height,  // height
                                        0, 0,                  // x and y corner radius
                                        Color.White,           // gradient start color
                                        0, 0,                  // gradient start coordinates  
                                        Color.Black,           // gradient end color
                                        backgroundImg.Width,   // gradient end x coordinate 
                                        backgroundImg.Height,  // gradient end y coordinate
                                        Bitmap.OpacityOpaque); // opacity
            //prepare a working buffer to hold graphics before flushing to display
            Bitmap bufferImg = new Bitmap(screenWidth, screenHeight);
            //our ball
            Bitmap soccerBallImg = Resources.GetBitmap(Resources.BitmapResources.SoccerBall);
            //make background of the ball transparent
            //using the color of top left corner pixel
            soccerBallImg.MakeTransparent(soccerBallImg.GetPixel(0, 0));

            int x = 100;
            int y = 50;
            int xOfs = 1;
            int yOfs = 1;
            while (true)
            {
                //copy background to buffer
                bufferImg.DrawImage(0, 0, backgroundImg, 0, 0,
                                    backgroundImg.Width, backgroundImg.Height);
                //paint moving sprite object
                bufferImg.DrawImage(x, y,     // destination coordinates
                                    soccerBallImg, // source image
                                    0, 0,       // source coordinates
                                    soccerBallImg.Width, soccerBallImg.Height,
                                    Bitmap.OpacityOpaque);
                bufferImg.Flush(); //flush buffer to display
                Thread.Sleep(10);
                //invert direction if ball bounces at a wall
                if (x <= 0 || x >= screenWidth - soccerBallImg.Width)
                    xOfs = -xOfs;
                if (y <= 0 || y >= screenHeight - soccerBallImg.Height)
                    yOfs = -yOfs;
                //calculate new coordinates
                x += xOfs;
                y += yOfs;
            }
        }
    }
}
