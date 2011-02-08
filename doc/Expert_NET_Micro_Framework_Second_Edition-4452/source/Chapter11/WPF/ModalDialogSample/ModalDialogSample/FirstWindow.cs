using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT;

namespace ModalDialogSample
{
    public class FirstWindow : Window
    {
        public FirstWindow()
        {
            this.Height = SystemMetrics.ScreenHeight;
            this.Width = SystemMetrics.ScreenWidth;

            // Create a single text control.
            Text text = new Text();
            text.Font = Resources.GetFont(Resources.FontResources.small);
            text.ForeColor = Color.Black;
            text.TextContent = "First Window - press button to show second window";
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
            // Show a modal dialog window

            // Create and show new window
            SecondWindow wnd = new SecondWindow();
            wnd.Visibility = Visibility.Visible;
            wnd.Topmost = true; // move on top of all others

            DispatcherFrame modalFrame = new DispatcherFrame();

            // Create a handler that will terminate the loop when the window will be hidden
            PropertyChangedEventHandler handler = delegate
            {
                if (wnd.Visibility != Visibility.Visible) // when the windows was hidden
                    modalFrame.Continue = false; // tell the frame to exit the loop
            };

            wnd.IsVisibleChanged += handler; // add handler to terminate the loop
            Dispatcher.PushFrame(modalFrame); // start the loop and block the calling thread
            // Here we go when the modal dialog was closed
            wnd.IsVisibleChanged -= handler; // remove handler
        }
    }
}
