using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IoT.RaspberryPi
{
    public class SimulationStreamer : IStreamer
    {
        private readonly ILogger<SimulationStreamer> _logger;

        public SimulationStreamer(ILogger<SimulationStreamer> logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
        }

        public void Start(string url, string streamName, TimeSpan duration)
        {
        }

        public void Stop()
        {
        }
    }
}
