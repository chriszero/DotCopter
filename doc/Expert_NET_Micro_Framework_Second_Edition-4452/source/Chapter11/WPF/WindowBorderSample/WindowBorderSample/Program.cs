using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;

namespace WindowBorderSample
{
    public class Program : Application
    {
        public static void Main()
        {
            Program myApplication = new Program();

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

            // Create a single text control.
            Text text = new Text();
            text.Font = Resources.GetFont(Resources.FontResources.small);
            text.TextContent = Resources.GetString(Resources.StringResources.String1);
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;

            Border border = new Border();
            border.SetBorderThickness(10); // set a 10 pixel border for r,l,t,b
            border.BorderBrush = new LinearGradientBrush(Colors.White,
                                                         Colors.Blue,
                                                         0, 0,
                                                         SystemMetrics.ScreenWidth,
                                                         SystemMetrics.ScreenHeight);
            border.Child = text; // Add the text element to the border control

            // Add the text control to the window.
            mainWindow.Child = border;

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            return mainWindow;
        }
    }
}
