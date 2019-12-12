using Microsoft.Extensions.Logging;
using System;

namespace IoT.RaspberryPi
{
    public class RaspberryPi : IRaspberryPi, IDisposable
    {
        private readonly ILogger<RaspberryPi> _logger;
        private readonly IGpio _gpio;
        private readonly IStreamer _streamer;

        public RaspberryPi(ILogger<RaspberryPi> logger, IGpio gpio, IStreamer streamer)
        {
            _logger = logger;
            _gpio = gpio;
            _streamer = streamer;
            _logger.LogDebug("Raspberry pi created");
        }

        public IGpio Gpio => _gpio;

        public IStreamer Streamer => _streamer;

        //TODO : Save on persistant
        public string SharedFolder { get; set; }

        #region IDisposable

        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            _logger.LogInformation("Raspberry pi disposing...");

            if (!_disposedValue)
            {
                if (disposing)
                {
                    _gpio.Dispose();
                    _streamer.Dispose();
                }

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
