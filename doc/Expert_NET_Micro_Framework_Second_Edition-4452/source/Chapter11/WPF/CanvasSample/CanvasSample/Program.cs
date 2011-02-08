using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;

namespace CanvasSample
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

            Canvas canvas = new Canvas();

            // Create a single text control and add it to the canvas
            Font font = Resources.GetFont(Resources.FontResources.small);
            Text text = new Text(font, "I am a racer.");
            Canvas.SetLeft(text, 50);
            Canvas.SetTop(text, 200);
            canvas.Children.Add(text);

            // Create an image and add it to the canvas
            Bitmap racer = Resources.GetBitmap(Resources.BitmapResources.Racer);
            Image image = new Image(racer);
            Canvas.SetLeft(image, 10);
            Canvas.SetTop(image, 10);
            canvas.Children.Add(image);

            // Create a rectangle shape and add it to the canvas
            Rectangle rect = new Rectangle();
            rect.Fill = new SolidColorBrush(Colors.Blue);
            rect.Width = 100;
            rect.Height = 50;
            Canvas.SetLeft(rect, 200);
            Canvas.SetTop(rect, 50);
            canvas.Children.Add(rect);

            // Add the canvas to the window.
            mainWindow.Child = canvas;

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            return mainWindow;
        }
    }
}
