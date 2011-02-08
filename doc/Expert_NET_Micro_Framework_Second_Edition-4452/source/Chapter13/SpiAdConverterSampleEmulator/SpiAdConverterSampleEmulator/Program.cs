using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.SPOT.Emulator;
using Kuehner.SPOT.Emulator;

namespace SpiAdConverterSampleEmulator
{
    class Program : Emulator
    {
        private ADC124S101 adc;

        public override void SetupComponent()
        {
            base.SetupComponent();
        }

        public override void InitializeComponent()
        {
            base.InitializeComponent();

            //check for required components of the configuration file
            this.adc = this.FindComponentById("ADC") as ADC124S101;
            if (this.adc == null)
                throw new Exception("The component 'ADC' is required by the emulator.");

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
            Application.Run(new Form1(adc));

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