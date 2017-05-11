using System;
using CodeConcepts.FrameworkExtensions.Interfaces.Utilities;
using MSStopwatch = System.Diagnostics.Stopwatch;

namespace CodeConcepts.FrameworkExtensions.Utilities
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