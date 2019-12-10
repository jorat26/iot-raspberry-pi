using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.RaspberryPi
{
    public interface IRaspberryPi : IDisposable
    {
        IGpio Gpio { get; }
        ICamera Camera { get; }
    }
}
