using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;

namespace ListBoxWithTextItemsSample
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

            // Create a list box control and add text items
            ListBox listBox = new ListBox();
            for (int i = 0; i < 10; ++i)
            {
                string str = "Item " + i + ". Hello World.";
                listBox.Items.Add(new Text(font, str));
            }

            // Add the text control to the window.
            mainWindow.Child = listBox;

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            // Let the user select items with the up and down buttons.
            Buttons.Focus(listBox);
            // Get notified when the selected item was changed.
            listBox.SelectionChanged += new SelectionChangedEventHandler(listBox_SelectionChanged);
            // Get notified when a selected item was pressed
            // using the select button.
            listBox.AddHandler(Buttons.ButtonDownEvent, new ButtonEventHandler(listBox_ButtonDown), false);

            return mainWindow;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            Debug.Print("Item " + args.SelectedIndex + " was selected.");
        }

        private void listBox_ButtonDown(object sender, ButtonEventArgs args)
        {
            ListBox listBox = (ListBox)sender;
            if(args.Button == Button.VK_SELECT)
                Debug.Print("Item " + listBox.SelectedIndex + " was pressed.");
        }
    }
}
