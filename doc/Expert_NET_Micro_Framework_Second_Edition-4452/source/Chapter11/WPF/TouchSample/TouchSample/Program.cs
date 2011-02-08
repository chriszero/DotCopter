using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Touch;

namespace TouchSample
{
    public class Program : Application
    {
        public static void Main()
        {
            Program myApplication = new Program();

            Window mainWindow = myApplication.CreateWindow();

            try
            {
                Touch.Initialize(myApplication);
            }
            catch (NotSupportedException)
            {
                Debug.Print("Touch displays not supported on the device.");
            }

            // Start the application
            myApplication.Run(mainWindow);
        }

        private Window mainWindow;

        public Window CreateWindow()
        {
            // Create a window object and set its size to the
            // size of the display.
            mainWindow = new Window();
            mainWindow.Height = SystemMetrics.ScreenHeight;
            mainWindow.Width = SystemMetrics.ScreenWidth;

            // Create a single text control.
            Text text = new Text();
            text.Font = Resources.GetFont(Resources.FontResources.NinaB);
            text.TextContent = "Touch me!";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;

            // Subscribe to touch events
            text.StylusDown += new StylusEventHandler(text_StylusDown);
            text.StylusMove += new StylusEventHandler(text_StylusMove);
            text.StylusUp += new StylusEventHandler(text_StylusUp);

            // Add the text control to the window.
            mainWindow.Child = text;

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            return mainWindow;
        }

        private void text_StylusDown(object sender, StylusEventArgs e)
        {
            Text txt = (Text)sender;
            Stylus.Capture(txt, CaptureMode.Element);
            int xrel, yrel;
            e.GetPosition(txt, out xrel, out yrel);
            txt.TextContent = "Down, screen={" + e.X + "," + e.Y + "}, relative={" + xrel + "," + yrel + "}"; 
        }

        private void text_StylusMove(object sender, StylusEventArgs e)
        {
            Text txt = (Text)sender;
            int xrel, yrel;
            e.GetPosition(txt, out xrel, out yrel);
            txt.TextContent = "Move, screen={" + e.X + "," + e.Y + "}, relative={" + xrel + "," + yrel + "}";
        }

        private void text_StylusUp(object sender, StylusEventArgs e)
        {
            Text txt = (Text)sender;
            Stylus.Capture(txt, CaptureMode.None);
            int xrel, yrel;
            e.GetPosition(txt, out xrel, out yrel);
            txt.TextContent = "Up, screen={" + e.X + "," + e.Y + "}, relative={" + xrel + "," + yrel + "}";
        }
    }
}
