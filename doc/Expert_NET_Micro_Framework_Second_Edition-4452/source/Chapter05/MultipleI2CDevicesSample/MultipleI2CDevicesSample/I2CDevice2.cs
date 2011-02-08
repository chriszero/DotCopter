using System;
using System.Runtime.CompilerServices;

namespace Microsoft.SPOT.Hardware
{
    public class I2CDevice2
    {
        public I2CDevice.Configuration Config;
        private bool isDisposed;

        private static I2CDevice singletonDevice;
        private static int instanceCount;

        public I2CDevice2(I2CDevice.Configuration config)
        {
            // In the constructor a 
            if (config == null)
                throw new ArgumentNullException();
            this.Config = config;
            if (singletonDevice == null)
                singletonDevice = new I2CDevice(config);
            instanceCount++;
        }

        public I2CDevice.I2CReadTransaction CreateReadTransaction(byte[] buffer)
        {
            if (this.isDisposed)
                throw new ObjectDisposedException();
            return singletonDevice.CreateReadTransaction(buffer);
        }

        public I2CDevice.I2CWriteTransaction CreateWriteTransaction(byte[] buffer)
        {
            if (this.isDisposed)
                throw new ObjectDisposedException();
            return singletonDevice.CreateWriteTransaction(buffer);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Dispose(bool fDisposing)
        {
            if (!this.isDisposed)
            {
                try
                {
                    if (instanceCount > 0)
                    {
                        instanceCount--;
                        if (instanceCount == 0)
                        {
                            if (singletonDevice != null)
                            {
                                singletonDevice.Dispose();
                                singletonDevice = null;
                            }
                        }
                    }
                }
                finally
                {
                    this.isDisposed = true;
                }
            }
        }

        public int Execute(I2CDevice.I2CTransaction[] xActions, int timeout)
        {
            if (this.isDisposed)
                throw new ObjectDisposedException();
            singletonDevice.Config = this.Config;
            return singletonDevice.Execute(xActions, timeout);
        }

        ~I2CDevice2()
        {
            this.Dispose(false);
        }
    }
}
