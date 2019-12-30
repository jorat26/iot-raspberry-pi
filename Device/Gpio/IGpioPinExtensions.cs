using System;
using System.IO;

namespace IoT.RaspberryPi
{
    public static class IGpioPinExtensions
    {
        public static void Pulse(this IGpioPin gpioPin, GpioValues value, int milliseconds)
        {
            switch (value)
            {
                case GpioValues.Low:
                    gpioPin.Write(GpioValues.Low);
                    System.Threading.Thread.Sleep(milliseconds);
                    gpioPin.Write(GpioValues.High);
                    break;
                case GpioValues.High:
                    gpioPin.Write(GpioValues.High);
                    System.Threading.Thread.Sleep(milliseconds);
                    gpioPin.Write(GpioValues.Low);
                    break;
            }
        }
    }
}
