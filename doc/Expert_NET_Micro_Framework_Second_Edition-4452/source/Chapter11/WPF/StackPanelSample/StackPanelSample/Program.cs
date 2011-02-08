using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;

namespace StackPanelSample
{
    public class Program : Microsoft.SPOT.Application
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

            StackPanel panel = new StackPanel(Orientation.Vertical);

            // Create a single text control and add it to the panel
            Font font = Resources.GetFont(Resources.FontResources.small);
            Text text = new Text(font, "I am a racer.");
            text.HorizontalAlignment = HorizontalAlignment.Left;
            panel.Children.Add(text);

            // Create an image and add it to the panel
            Bitmap racer = Resources.GetBitmap(Resources.BitmapResources.Racer);
            Image image = new Image(racer);
            image.HorizontalAlignment = HorizontalAlignment.Left;
            panel.Children.Add(image);

            // Create a rectangle shape and add it to the panel
            Rectangle rect = new Rectangle();
            rect.HorizontalAlignment = HorizontalAlignment.Left;
            rect.Fill = new SolidColorBrush(Colors.Blue);
            rect.Width = 100;
            rect.Height = 50;
            panel.Children.Add(rect);

            // Add the panel to the window.
            mainWindow.Child = panel;

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            return mainWindow;
        }
    }
}
