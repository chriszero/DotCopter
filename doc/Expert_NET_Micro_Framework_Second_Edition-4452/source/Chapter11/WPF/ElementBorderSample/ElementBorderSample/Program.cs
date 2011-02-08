using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;

namespace ElementBorderSample
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

            Canvas canvas = new Canvas();

            // Create aborder for the text
            Border textBorder = new Border();
            textBorder.SetBorderThickness(1, 5, 1, 5);
            textBorder.BorderBrush = new SolidColorBrush(Colors.Blue);
            Canvas.SetLeft(textBorder, 20);
            Canvas.SetTop(textBorder, 150);

            // Create a single text control and add it to the canvas
            Font font = Resources.GetFont(Resources.FontResources.NinaB);
            Text text = new Text(font, "Text with a border");
            textBorder.Child = text; // Add the text to the border

            // Add the border to the canvas
            canvas.Children.Add(textBorder);

            // Add the canvas to the window.
            mainWindow.Child = canvas;

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            return mainWindow;
        }
    }
}
