using Microsoft.SPOT;

namespace ModalDialogSample
{
    public class Program
    {
        public static void Main()
        {
            FirstWindow firstWindow = new FirstWindow();

            // Create the object that configures the GPIO pins to buttons.
            GPIOButtonInputProvider inputProvider = new GPIOButtonInputProvider(null);

            Application myApplication = new Application();
            // Start the application
            myApplication.Run(firstWindow);
        }
    }
}
