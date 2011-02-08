using System;
using Kuehner.SPOT.Presentation.Controls;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;

namespace ComplexListBoxSample
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

            Font normalFont = Resources.GetFont(Resources.FontResources.NinaB);
            Font smallFont = Resources.GetFont(Resources.FontResources.small);

            // Create a list box control and add text items
            ListBox listBox = new ListBox();
            // set the width so that it fills the entire screen
            listBox.Child.Width = mainWindow.Width;
            // make the list box transparent
            listBox.Background = null;
            // make the enclosed scroll viewer transparent also
            // we get the scroll viewer via the child property but
            // need to cast it to Control in order to clear the background
            ((Control)listBox.Child).Background = null;

            // Add simple text items
            for (int i = 0; i < 2; ++i)
            {
                string str = "Simple text item";
                Text text = new Text(normalFont, str);
                text.SetMargin(2);
                ListBoxItem item = new HighlightableListBoxItem(text);
                listBox.Items.Add(item);
            }

            // Add a separator
            listBox.Items.Add(new SeparatorListBoxItem());

            // Add a text item with icon
            {
                // Create the stack panel to align the elements
                StackPanel stackPanel = new StackPanel(Orientation.Horizontal);

                // Icon
                Bitmap bmp = Resources.GetBitmap(Resources.BitmapResources.Clock);
                // Make the bitmap transparent using
                // the color of the top left corner pixel.
                // Therefore the image should not be in the Bitmap and Jpeg format
                // because that requires to create a copy in order to make it
                // transparent. Use Gif instead.
                bmp.MakeTransparent(bmp.GetPixel(0, 0));
                Image image = new Image(bmp);
                image.SetMargin(2); // set a margin to separate the image
                // vertically center the icon within the item
                image.VerticalAlignment = VerticalAlignment.Center;
                stackPanel.Children.Add(image);

                // Text
                Text text = new Text(normalFont, "Item with a icon and text");
                text.SetMargin(2); // set margin to separate the text
                // vertically center the icon within the item
                text.VerticalAlignment = VerticalAlignment.Center;
                stackPanel.Children.Add(text);

                // Create a highlightable list box item
                ListBoxItem item = new HighlightableListBoxItem(stackPanel);
                listBox.Items.Add(item);
            }

            // Add a separator
            listBox.Items.Add(new SeparatorListBoxItem());

            // Add two items with multiple columns
            // use i to add a right aligned number to the first column
            for (int i = 0; i <= 100; i += 50)
            {
                //create the stack panel to align the elements
                StackPanel stackPanel = new StackPanel(Orientation.Horizontal);

                // Add right aligned text
                Text text1 = new Text(normalFont, i.ToString());
                text1.Width = 30;
                text1.SetMargin(2); // set margin to separate the text
                text1.TextAlignment = TextAlignment.Right;
                // vertically center the icon within the item
                text1.VerticalAlignment = VerticalAlignment.Center;
                stackPanel.Children.Add(text1);

                // Icon
                Bitmap bmp = Resources.GetBitmap(Resources.BitmapResources.Audio);
                // Make the bitmap transparent using
                // the color of the top left corner pixel.
                // Therefore the image should not be in the Bitmap and Jpeg format
                // because that requires to create a copy in order to make it
                // transparent. Use Gif instead.
                bmp.MakeTransparent(bmp.GetPixel(0, 0));
                Image image = new Image(bmp);
                image.SetMargin(2); // set a margin to separate the image
                // vertically center the icon within the item
                image.VerticalAlignment = VerticalAlignment.Center;
                stackPanel.Children.Add(image);

                // Text
                Text text = new Text(normalFont, "Item with multiple columns");
                text.SetMargin(2); // set margin to separate the text
                // vertically center the icon within the item
                text.VerticalAlignment = VerticalAlignment.Center;
                stackPanel.Children.Add(text);

                // Create a highlightable list box item
                ListBoxItem item = new HighlightableListBoxItem(stackPanel);
                listBox.Items.Add(item);
            }

            // Add a separator
            listBox.Items.Add(new SeparatorListBoxItem());

            // Add two multi line text item
            for (int i = 0; i < 2; ++i)
            {
                TextFlow textFlow = new TextFlow();
                textFlow.TextRuns.Add("This is the first line.", normalFont, Colors.Black);
                textFlow.TextRuns.Add(TextRun.EndOfLine);
                textFlow.TextRuns.Add("Second line.", normalFont, Colors.Green);
                textFlow.TextRuns.Add(TextRun.EndOfLine);
                textFlow.TextRuns.Add("Third line.", smallFont, Colors.Red);
                textFlow.SetMargin(2);

                ListBoxItem item = new HighlightableListBoxItem(textFlow);
                listBox.Items.Add(item);
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
