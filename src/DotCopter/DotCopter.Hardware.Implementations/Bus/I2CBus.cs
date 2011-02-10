using System;
using Microsoft.SPOT.Hardware;

namespace DotCopter.Hardware.Implementations.Bus
{
    public class I2CBus
    {
       private readonly I2CDevice _slaveDevice;

        public I2CBus()
        {
            _slaveDevice = new I2CDevice(new I2CDevice.Configuration(0, 0));
        }

        public void Write(I2CDevice.Configuration config, byte[] buffer, int transactionTimeout)
        {
            _slaveDevice.Config = config;
            I2CDevice.I2CTransaction[] i2CTransactions = new I2CDevice.I2CTransaction[] { I2CDevice.CreateWriteTransaction(buffer) };
            lock (_slaveDevice)
                _slaveDevice.Execute(i2CTransactions, transactionTimeout);
        }

        public void Read(I2CDevice.Configuration config, byte[] buffer, int transactionTimeout)
        {
            _slaveDevice.Config = config;
            I2CDevice.I2CTransaction[] i2CTransactions = new I2CDevice.I2CTransaction[] { I2CDevice.CreateReadTransaction(buffer) };
            lock (_slaveDevice)
                _slaveDevice.Execute(i2CTransactions, transactionTimeout);
        }

        public void ReadRegister(I2CDevice.Configuration config, byte register, byte[] buffer, int transactionTimeout)
        {
            _slaveDevice.Config = config;
            I2CDevice.I2CTransaction[] i2CTransactions = new I2CDevice.I2CTransaction[]
                                                             {
                                                                 I2CDevice.CreateWriteTransaction(new byte[]{register}),
                                                                 I2CDevice.CreateReadTransaction(buffer)
                                                             };
            lock (_slaveDevice)
                _slaveDevice.Execute(i2CTransactions, transactionTimeout);
        }

        public void WriteRegister(I2CDevice.Configuration config, byte register, byte[] buffer, int transactionTimeout)
        {
            _slaveDevice.Config = config;
            I2CDevice.I2CTransaction[] i2CTransactions = new I2CDevice.I2CTransaction[]
                                                             {
                                                                 I2CDevice.CreateWriteTransaction(new byte[] {register})
                                                                 ,
                                                                 I2CDevice.CreateWriteTransaction(buffer)
                                                             };
            lock (_slaveDevice)
                _slaveDevice.Execute(i2CTransactions, transactionTimeout);
        }
    }
}