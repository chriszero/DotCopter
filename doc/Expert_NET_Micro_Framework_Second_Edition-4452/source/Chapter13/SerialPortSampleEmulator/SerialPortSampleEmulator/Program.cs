using System;
using System.Windows.Forms;
using System.Threading;

using Microsoft.SPOT.Emulator;
using Microsoft.SPOT.Emulator.Com;

namespace SerialPortSampleEmulator
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

            if (FindComponentById("COM1") as ComPortToMemoryStream == null)
                throw new Exception("The component 'COM1' is required by the emulator.");

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
            Application.Run(new Form1(this)); //jku

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