using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;

namespace ScrollViewerSample
{
    public class Program : Microsoft.SPOT.Application
    {
        public static void Main()
        {
            Program myApplication = new Program();

            Window mainWindow = myApplication.CreateWindow();

            // Create the object that configures the GPIO pins to buttons.
            GPIOButtonInputProvider inputProvider = new GPIOButtonInputProvider(null);

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

            // Create a scrollviewer
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Background = new SolidColorBrush(Colors.Gray);
            // scroll line by line with 10 pixels per line
            scrollViewer.ScrollingStyle = ScrollingStyle.LineByLine;
            scrollViewer.LineWidth = 10;
            scrollViewer.LineHeight = 10;

            // Create a canvas and add ellipse shapes
            Canvas canvas = new Canvas();
            for (int x = 0; x <= 20; ++x)
            {
                for (int y = 0; y <= 20; ++y)
                {
                    Ellipse ellipse = new Ellipse(10, 10);
                    ellipse.Stroke = new Pen(Colors.White);
                    canvas.Children.Add(ellipse);
                    Canvas.SetLeft(ellipse, x * 30);
                    Canvas.SetTop(ellipse, y * 30);
                }
            }
            //we need to set the size of a canvas explicitly
            //because it doesn´t calculate the desired size from its content
            canvas.Width = 20 * 30 + 10 * 2;
            canvas.Height = 20 * 30 + 10 * 2;
            scrollViewer.Child = canvas;

            // Add the scroll viewer to the window.
            mainWindow.Child = scrollViewer;

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            // Attach the button focus to the scroll viewer
            // to be able to scroll with the up down right and left buttons
            Buttons.Focus(scrollViewer);

            return mainWindow;
        }
    }
}
