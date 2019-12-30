using System;
using System.IO;

namespace IoT.RaspberryPi
{
    public enum GpioModes
    {
        Input,
        Output
    }

    public enum GpioValues
    {
        Low = 0,
        High = 1
    }

    public class GpioPin : IGpioPin, IGpioStatus, IDisposable
    {
        private const string DevicePath = @"/sys/class/gpio";
        private readonly string _gpioPath;

        public GpioPin(int pinNumber)
        {
            if (pinNumber < 1 || pinNumber > 27)
                throw new ArgumentOutOfRangeException("Valid pins are between 1 and 27.");

            PinNumber = pinNumber;
            _gpioPath = Path.Combine(DevicePath, string.Concat("gpio", PinNumber.ToString()));
        }

        public int PinNumber { get; }

        public bool IsOpen => Directory.Exists(_gpioPath);

        public GpioModes Mode { get; private set; }

        public void Open()
        {
            var gpioExportPath = Path.Combine(DevicePath, "export");
            if (!Directory.Exists(_gpioPath))
            {
                File.WriteAllText(gpioExportPath, PinNumber.ToString());
                Directory.CreateDirectory(_gpioPath);
            }
        }

        public void Close()
        {
            var gpioExportPath = Path.Combine(DevicePath, "unexport");
            if (Directory.Exists(_gpioPath))
            {
                File.WriteAllText(gpioExportPath, PinNumber.ToString());
                Directory.Delete(_gpioPath);
            }
        }

        public void SetMode(GpioModes mode)
        {
            CheckIsOpen();

            switch (mode)
            {
                case GpioModes.Input:
                    File.WriteAllText(Path.Combine(_gpioPath, "direction"), "in");
                    Directory.SetLastWriteTime(Path.Combine(_gpioPath), DateTime.UtcNow);
                    break;
                case GpioModes.Output:
                    File.WriteAllText(Path.Combine(_gpioPath, "direction"), "out");
                    Directory.SetLastWriteTime(Path.Combine(_gpioPath), DateTime.UtcNow);
                    break;
            }

            Mode = mode;
        }

        public void Write(GpioValues value)
        {
            CheckIsOpen();
            CheckIsOutput();

            File.WriteAllText(Path.Combine(_gpioPath, "value"), ((int)value).ToString());
            Directory.SetLastWriteTime(Path.Combine(_gpioPath), DateTime.UtcNow);
        }

        public GpioValues Read()
        {
            CheckIsOpen();

            if (File.Exists(Path.Combine(_gpioPath, "value")))
            {
                var value = File.ReadAllText(Path.Combine(_gpioPath, "value"));
                return (GpioValues)Enum.Parse(typeof(GpioValues), value);
            }
            else
            {
                return GpioValues.Low;
            }
        }

        private void CheckIsOpen()
        {
            if (!IsOpen)
            {
                throw new GpioPinException($"Pin {PinNumber} must be open to allow mode changes.");
            }
        }

        private void CheckIsOutput()
        {
            if (Mode == GpioModes.Output)
            {
                throw new GpioPinException($"Pin {PinNumber} must be configured as an output to be able to write.");
            }
        }

        #region IDisposable

        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                    Close();

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
