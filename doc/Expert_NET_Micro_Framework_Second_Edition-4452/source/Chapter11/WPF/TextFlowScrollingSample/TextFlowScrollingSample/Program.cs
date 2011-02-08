using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;

namespace TextFlowScrollingSample
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

            Font normalFont = Resources.GetFont(Resources.FontResources.NinaB);
            Font smallFont = Resources.GetFont(Resources.FontResources.small);

            TextFlow textFlow = new TextFlow();
            // set the scrolling style to LineByLine or PageByPage
            textFlow.ScrollingStyle = ScrollingStyle.LineByLine;
            // Add text
            Color[] colors = new Color[] { Colors.Black, Colors.Gray,
                                           Colors.Red, Colors.Green, Colors.Blue};
            for (int i = 0; i < 100; ++i)
            {
                Font font = (i % 2 == 0) ? normalFont : smallFont;
                Color color = colors[i % colors.Length];
                textFlow.TextRuns.Add("Hello world. ", font, color);
                if (i % 2 == 0)
                    textFlow.TextRuns.Add(TextRun.EndOfLine);
            }

            // Add the text flow to the window.
            mainWindow.Child = textFlow;

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            // Let the user scroll the text with buttons 
            Buttons.Focus(textFlow);

            return mainWindow;
        }
    }
}
