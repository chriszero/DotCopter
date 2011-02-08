using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;

namespace DispatcherTimerSample
{
    public class Program : Application
    {
        private int count;
        private Text textElement;
        private DispatcherTimer dispatcherTimer;

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
            this.textElement = new Text();
            this.textElement.Font = Resources.GetFont(Resources.FontResources.NinaB);
            this.textElement.TextContent = this.count.ToString();
            this.textElement.HorizontalAlignment = HorizontalAlignment.Center;
            this.textElement.VerticalAlignment = VerticalAlignment.Center;
            // Add the text control to the window.
            mainWindow.Child = this.textElement;

            this.dispatcherTimer = new DispatcherTimer(this.textElement.Dispatcher);
            this.dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            this.dispatcherTimer.Interval = new TimeSpan(0, 0, 1); //one second
            this.dispatcherTimer.Start();

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            // Attach the button focus to the window.
            Buttons.Focus(mainWindow);

            return mainWindow;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Update the text element
            this.count++;
            this.textElement.TextContent = this.count.ToString();
        }
    }
}
