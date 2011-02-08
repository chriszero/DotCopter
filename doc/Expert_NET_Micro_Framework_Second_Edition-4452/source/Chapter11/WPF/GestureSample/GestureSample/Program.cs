using Microsoft.SPOT;
using Microsoft.SPOT.Ink;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Touch;

namespace GestureSample
{
    public class Program : Application
    {
        public static void Main()
        {
            Program myApplication = new Program();

            Touch.Initialize(myApplication);
            TouchCollectorConfiguration.CollectionMethod = CollectionMethod.Native;
            TouchCollectorConfiguration.CollectionMode = CollectionMode.GestureOnly;

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

            // Create Ink Canvas
            InkCanvas inkCanvas = new InkCanvas(0, 0, mainWindow.Width, mainWindow.Height);
            // Subscribe to event handler to get notified of gestures
            inkCanvas.Gesture += new InkCollectorGestureEventHandler(inkCanvas_Gesture);

            // Add the text control to the window.
            mainWindow.Child = inkCanvas;

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            return mainWindow;
        }

        private void inkCanvas_Gesture(object sender, InkCollectorGestureEventArgs e)
        {
            switch(e.Gesture.Id)
            {
                case ApplicationGesture.Right:
                    Debug.Print("Right");
                    break;
                case ApplicationGesture.UpRight:
                    Debug.Print("UpRight");
                    break;
                case ApplicationGesture.Up:
                    Debug.Print("Up");
                    break;
                case ApplicationGesture.UpLeft:
                    Debug.Print("UpLeft");
                    break;
                case ApplicationGesture.Left:
                    Debug.Print("Left");
                    break;
                case ApplicationGesture.DownLeft:
                    Debug.Print("DownLeft");
                    break;
                case ApplicationGesture.Down:
                    Debug.Print("Down");
                    break;
                case ApplicationGesture.DownRight:
                    Debug.Print("DownRight");
                    break;
                default:
                    Debug.Print("Unknown");
                    break;
            }
        }
    }
}
