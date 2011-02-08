using System;
using Kuehner.SPOT.Presentation.Controls;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;

namespace ListBoxBackgroundSample
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
            // Add a gradient color background to the window
            mainWindow.Background = new LinearGradientBrush(Colors.White, Colors.Red,
                                                            0, 0,
                                                            mainWindow.Width, mainWindow.Height);

            Font font = Resources.GetFont(Resources.FontResources.NinaB);

            // Create a list box control and add text items
            ListBox listBox = new ListBox();
            // make the list box transparent
            listBox.Background = null;
            // make the enclosed scroll viewer transparent also
            // we get the scroll viewer via the child property but
            // need to cast it to Control in order to clear the background
            ((Control)listBox.Child).Background = null;
            for (int i = 0; i < 10; ++i)
            {
                string str = "Item " + i + ". Hello World.";
                ListBoxItem item = new HighlightableTextListBoxItem(font, str);
                listBox.Items.Add(item);
                if (i > 0 && i % 4 == 0)
                    listBox.Items.Add(new SeparatorListBoxItem());
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
            if (args.Button == Button.VK_SELECT)
                Debug.Print("Item " + listBox.SelectedIndex + " was pressed.");
        }
    }
}
