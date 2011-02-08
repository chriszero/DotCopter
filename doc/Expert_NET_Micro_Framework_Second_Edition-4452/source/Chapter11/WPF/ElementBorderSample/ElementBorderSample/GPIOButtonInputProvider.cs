using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation;

namespace ElementBorderSample
{
    // This class dispatches input events from hardware (or emulated) buttons. It uses
    // the hardware provider object to query the device for named buttons pins.
    public sealed class GPIOButtonInputProvider
    {
        public readonly Dispatcher Dispatcher;

        private ButtonPad[] buttons;
        private ReportInputCallback callback;
        private InputProviderSite site;
        private PresentationSource source;

        private delegate bool ReportInputCallback(InputReport inputReport);

        // This class maps GPIOs to Buttons processable by Microsoft.SPOT.Presentation
        public GPIOButtonInputProvider(PresentationSource source)
        {
            // Set the input source.
            this.source = source;
            // Register our object as an input source with the input manager and get back an
            // InputProviderSite object which forwards the input report to the input manager,
            // which then places the input in the staging area.
            site = InputManager.CurrentInputManager.RegisterInputProvider(this);
            // Create a delegate that refers to the InputProviderSite object's ReportInput method
            callback = new ReportInputCallback(site.ReportInput);
            Dispatcher = Dispatcher.CurrentDispatcher;

            // Create a hardware provider
            HardwareProvider hwProvider = new HardwareProvider();

            // Create the pins we will need for the buttons
            // Default their values for the emulator
            Cpu.Pin pinLeft = Cpu.Pin.GPIO_Pin0;
            Cpu.Pin pinRight = Cpu.Pin.GPIO_Pin1;
            Cpu.Pin pinUp = Cpu.Pin.GPIO_Pin2;
            Cpu.Pin pinSelect = Cpu.Pin.GPIO_Pin3;
            Cpu.Pin pinDown = Cpu.Pin.GPIO_Pin4;

            // Use the hardware provider to get the pins
            // If the left pin is not set then assume none of them are
            // and set the left pin back to the default emulator value
            if ((pinLeft = hwProvider.GetButtonPins(Button.VK_LEFT)) == Cpu.Pin.GPIO_NONE)
                pinLeft = Cpu.Pin.GPIO_Pin0;
            else
            {
                pinRight = hwProvider.GetButtonPins(Button.VK_RIGHT);
                pinUp = hwProvider.GetButtonPins(Button.VK_UP);
                pinSelect = hwProvider.GetButtonPins(Button.VK_SELECT);
                pinDown = hwProvider.GetButtonPins(Button.VK_DOWN);
            }

            // Allocate button pads and assign the (emulated) hardware pins as input 
            // from specific buttons.
            ButtonPad[] buttons = new ButtonPad[]
            {
                // Associate the buttons to the pins as discovered or set above
                new ButtonPad(this, Button.VK_LEFT  , pinLeft),
                new ButtonPad(this, Button.VK_RIGHT , pinRight),
                new ButtonPad(this, Button.VK_UP    , pinUp),
                new ButtonPad(this, Button.VK_SELECT, pinSelect),
                new ButtonPad(this, Button.VK_DOWN  , pinDown),
            };

            this.buttons = buttons;
        }

        // The emulated device provides a button pad containing five buttons 
        // for user input. This class represents the button pad.
        internal class ButtonPad
        {
            private Button button;
            private InterruptPort port;
            private GPIOButtonInputProvider sink;

            // Construct the object. Set this class to handle the emulated 
            // hardware's button interrupts.
            public ButtonPad(GPIOButtonInputProvider sink, Button button, Cpu.Pin pin)
            {
                this.sink = sink;
                this.button = button;

                // When this GPIO pin is true, call the Interrupt method.
                port = new InterruptPort(pin, true, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth);
                port.OnInterrupt += new NativeEventHandler(this.Interrupt);
            }

            void Interrupt(uint data1, uint data2, TimeSpan time)
            {
                RawButtonActions action = (data2 != 0) ? RawButtonActions.ButtonUp : RawButtonActions.ButtonDown;

                RawButtonInputReport report = new RawButtonInputReport(sink.source, time, button, action);

                // Queue the button press to the input provider site.
                sink.Dispatcher.BeginInvoke(sink.callback, report);
            }
        }
    }
}


