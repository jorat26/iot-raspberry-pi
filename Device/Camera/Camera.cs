using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Woopsa;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace IoT.RaspberryPi
{
    public class Camera : ICamera
    {
        public const string Folder = "/home/pi/share/camera";
        public const string Image = "image";
        public const string Video = "video";

        private readonly ILogger<RaspberryPi> _logger;

        public Camera(ILogger<RaspberryPi> logger)
        {
            _logger = logger;
        }

        public string LastImageFilename { get; private set; }

        public string LastVideoFilename { get; private set; }

        public string SharedFolder => Folder;

        public void GrabImage()
        {

        }  

        // public async Task GrabImageAsync()
        // {
  
        // }

        public void Timelapse(TimeSpan interval, TimeSpan timeout)
        { 

        }  

        public void GrabVideo(TimeSpan duration)
        {

        }  

        public void StartStreaming(string url, string streamName, TimeSpan duration)
        {

        }

        public void StartStreamingAsync(string url, string streamName, TimeSpan duration)
        {

        }

        private FileInfo GetLastFile(string folder)
        {
            var directory = new DirectoryInfo(folder);
            return directory.GetFiles()
                .OrderByDescending(f => f.LastWriteTime)
                .First();
        }
    }
}
