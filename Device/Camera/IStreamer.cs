using System;

namespace IoT.RaspberryPi
{
    public interface IStreamer : IDisposable
    {
        void Start(string url, string streamName, TimeSpan duration);
        void Stop();
    }
}