using MSStopwatch = System.Diagnostics.Stopwatch;
using System;
using FrameworkExtensions.Interfaces.Utilities;

namespace FrameworkExtensions.Utilities
{
    public class Stopwatch : IStopwatch
    {
        private readonly MSStopwatch _stopwatch;

        internal Stopwatch()
        {
            _stopwatch = new MSStopwatch();
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }

        public void Reset()
        {
            _stopwatch.Reset();
        }

        public TimeSpan Elapsed => _stopwatch.Elapsed;
    }
}