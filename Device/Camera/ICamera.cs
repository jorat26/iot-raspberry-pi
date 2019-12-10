using System;

namespace IoT.RaspberryPi
{
    public interface ICamera
    {
        void GrabImage();
        void GrabVideo(TimeSpan duration);
        void StartStreaming(string url, string streamName, TimeSpan duration);
        string SharedFolder { get; }
    }
}