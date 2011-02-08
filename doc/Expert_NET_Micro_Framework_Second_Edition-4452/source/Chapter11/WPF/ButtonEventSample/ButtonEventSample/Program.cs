using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;

namespace ButtonEventSample
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

            Font font = Resources.GetFont(Resources.FontResources.NinaB);

            TextFlow textFlow = new TextFlow();
            textFlow.TextRuns.Add("Hello world.", font, Colors.Black);
            textFlow.TextRuns.Add(" Hello world.", font, Colors.Red);
            textFlow.TextRuns.Add(TextRun.EndOfLine);
            textFlow.TextRuns.Add("Hello world.", font, Colors.Green);

            // Add the text flow to the window.
            mainWindow.Child = textFlow;

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            textFlow.AddHandler(Buttons.ButtonDownEvent, new ButtonEventHandler(textFlow_ButtonDown), false);
            Buttons.Focus(textFlow);

            return mainWindow;
        }

        private void textFlow_ButtonDown(object sender, ButtonEventArgs args)
        {
            Debug.Print("Button: " + args.Button);
        }
    }
}
