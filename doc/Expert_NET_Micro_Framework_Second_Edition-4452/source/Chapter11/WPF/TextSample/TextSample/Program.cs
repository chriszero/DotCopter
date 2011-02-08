using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;

namespace TextSample
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

            // Create a single text control.
            Text text = new Text();

            text.Font = Resources.GetFont(Resources.FontResources.small);
            text.TextContent = "Displaying a text is one of the most common tasks you need to do with your display. So it is worth looking at the Text and TextFlow classes.";
            //text.Height = 100;
            //text.Width = 50;
            text.TextWrap = true;
            text.Trimming = Microsoft.SPOT.Presentation.Media.TextTrimming.WordEllipsis;
            text.TextAlignment = Microsoft.SPOT.Presentation.Media.TextAlignment.Right;
            text.HorizontalAlignment = Microsoft.SPOT.Presentation.HorizontalAlignment.Right;
            text.VerticalAlignment = Microsoft.SPOT.Presentation.VerticalAlignment.Center;

            // Add the text control to the window.
            mainWindow.Child = text;

            // Connect the button handler to all of the buttons.
            mainWindow.AddHandler(Buttons.ButtonUpEvent, new ButtonEventHandler(OnButtonUp), false);

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            // Attach the button focus to the window.
            Buttons.Focus(mainWindow);

            return mainWindow;
        }

        private void OnButtonUp(object sender, ButtonEventArgs e)
        {
            // Print the button code to the Visual Studio output window.
            Debug.Print(e.Button.ToString());
        }
    }
}
