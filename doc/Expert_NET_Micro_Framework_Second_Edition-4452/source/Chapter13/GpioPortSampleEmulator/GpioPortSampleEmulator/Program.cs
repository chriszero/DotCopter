using System;
using System.Windows.Forms;
using System.Threading;

using Microsoft.SPOT.Emulator;
using Microsoft.SPOT.Emulator.Gpio;

namespace GpioPortSampleEmulator
{
    class Program : Emulator
    {
        public override void SetupComponent()
        {
            base.SetupComponent();
        }

        public override void InitializeComponent()
        {
            base.InitializeComponent();

            if (FindComponentById("GPIOOut0") as GpioPort == null)
                throw new Exception("The component 'GPIOOut0' is required by the emulator.");
            if (FindComponentById("GPIOOut1") as GpioPort == null)
                throw new Exception("The component 'GPIOOut1' is required by the emulator.");
            if (FindComponentById("GPIOIn0") as GpioPort == null)
                throw new Exception("The component 'GPIOIn0' is required by the emulator.");
            if (FindComponentById("GPIOIn1") as GpioPort == null)
                throw new Exception("The component 'GPIOIn1' is required by the emulator.");
        
            // Start the UI in its own thread.
            Thread uiThread = new Thread(StartForm);
            uiThread.Start();
        }

        public override void UninitializeComponent()
        {
            base.UninitializeComponent();

            // The emulator is stopped. Close the WinForm UI.
            Application.Exit();
        }

        [STAThread]
        private void StartForm()
        {
            // Some initial setup for the WinForm UI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Start the WinForm UI. Run() returns when the form is closed.
            Application.Run(new Form1(this));

            // When the user closes the WinForm UI, stop the emulator.
            Stop();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            (new Program()).Start();
        }
    }
}