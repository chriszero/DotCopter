using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;

namespace TextFlowSample
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

            Font font = Resources.GetFont(Resources.FontResources.NinaB);
            Font smallFont = Resources.GetFont(Resources.FontResources.small);

            TextFlow textFlow = new TextFlow();
            textFlow.TextRuns.Add("Hello world.", font, Colors.Black);
            textFlow.TextRuns.Add(" Hello world.", smallFont, Colors.Red);
            textFlow.TextRuns.Add(TextRun.EndOfLine);
            textFlow.TextRuns.Add("Hello world.", font, Colors.Green);

            // Add the text flow to the window.
            mainWindow.Child = textFlow;

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            return mainWindow;
        }
    }
}
