using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.RaspberryPi
{
    public interface IGpio : IDisposable
    {
        IEnumerable<GpioPin> Pins { get; }
    }
}
