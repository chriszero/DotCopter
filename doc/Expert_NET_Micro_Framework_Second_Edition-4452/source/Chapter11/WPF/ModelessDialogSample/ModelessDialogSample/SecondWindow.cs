using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;

namespace ModelessDialogSample
{
    public class SecondWindow : Window
    {
        public SecondWindow()
        {
            this.Height = SystemMetrics.ScreenHeight;
            this.Width = SystemMetrics.ScreenWidth;

            // Create a single text control.
            Text text = new Text();
            text.Font = Resources.GetFont(Resources.FontResources.small);
            text.ForeColor = Color.Black;
            text.TextContent = "Second Window - press button to close it";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;

            // Add the text control to the window.
            this.Child = text;

            // Set the window visibility to visible.
            this.Visibility = Visibility.Visible;

            // Attach the button focus to the window.
            Buttons.Focus(this);

            // add button event handler
            AddHandler(Buttons.ButtonUpEvent, new ButtonEventHandler(OnButtonUp), false);
        }

        private void OnButtonUp(object sender, ButtonEventArgs e)
        {
            // hide window
            this.Visibility = Visibility.Hidden;
            // but this finally removes it
            Close();
        }
    }
}
