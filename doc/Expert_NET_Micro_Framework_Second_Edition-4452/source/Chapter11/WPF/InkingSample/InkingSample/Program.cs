using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Touch;
using Microsoft.SPOT.Ink;
using Microsoft.SPOT.Presentation.Media;

namespace InkingSample
{
    public class Program : Application
    {
        public static void Main()
        {
            Program myApplication = new Program();

            Touch.Initialize(myApplication);
            TouchCollectorConfiguration.CollectionMethod = CollectionMethod.Native;
            TouchCollectorConfiguration.CollectionMode = CollectionMode.InkOnly;
            TouchCollectorConfiguration.SetStylusMoveFrequency(5);

            Window mainWindow = myApplication.CreateWindow();

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

            // Create Ink Canvas
            InkCanvas inkCanvas = new InkCanvas(0, 0, mainWindow.Width, mainWindow.Height);
            // Set line color to green
            DrawingAttributes attr = new DrawingAttributes();
            attr.Color = Colors.Green;
            inkCanvas.DefaultDrawingAttributes = attr;

            // Add the text control to the window.
            mainWindow.Child = inkCanvas;

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            return mainWindow;
        }
    }
}
