using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace IoT.RaspberryPi
{
    public class CameraStreamer : IStreamer, IDisposable
    {
        public const string Filename = "/home/pi/stream/pic.jpg";
        public const string RaspistillCommand = "raspistill";
        public const string MjpgStreamerCommand = "/usr/local/bin/mjpg_streamer";

        private readonly ILogger<CameraStreamer> _logger;
        private readonly IList<Process> _processes;

        public CameraStreamer(ILogger<CameraStreamer> logger)
        {
            _logger = logger;
            _processes = new List<Process>();
        }

        public void Start(string url, string streamName, TimeSpan duration)
        {
            _logger.LogInformation("Streaming has started, two childs processes are created.");
            StartProcess(RaspistillCommand, $"--nopreview -w 640 -h 480 -q 5 -o {Filename} -tl 500 -t 9999999");
            StartProcess(MjpgStreamerCommand, $"-i \"input_file.so -f /home/pi/stream/ -n pic.jpg\"");
        }

        public void Stop()
        {
            _logger.LogInformation("Streaming is stopped, the childs processes are killed.");
            KillProcesses();
        }

        private void StartProcess(string command, string arguments)
        {
            _logger.LogInformation($"Start process with command : '{command} {arguments}'");

            Process process = null;
            try
            {
                process = new Process();
                process.StartInfo.FileName = command;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.OutputDataReceived += (sender, e) => _logger.LogInformation($"{command}: {e.Data}");
                process.ErrorDataReceived += (sender, e) => _logger.LogError($"{command}: {e.Data}");
                process.Start();

                _logger.LogInformation($"Process successfully started");
            }
            catch (Exception e)
            {
                process = null;
                _logger.LogCritical(e.Message);
            }
            finally
            {
                if (process != null)
                {
                    _processes.Add(process);
                }
            }
        }

        private void KillProcesses()
        {
            foreach (Process process in _processes)
            {
                _logger.LogInformation($"Kill process '{process.StartInfo.FileName}'");
                process.Kill();
            }
        }

        #region IDisposable

        private bool _disposedValue = false;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Stop();
                }

                _disposedValue = true;
            }
        }


        #endregion
    }
}
