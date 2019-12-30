using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.RaspberryPi
{
    public interface IGpio : IDisposable
    {
        IGpioPin this[int index] { get; }

        IEnumerable<GpioPin> Pins { get; }
    }
}
