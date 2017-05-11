using System;

namespace FrameworkExtensions.Interfaces.Utilities
{
    public interface IStopwatch
    {
        void Start();

        void Stop();

        void Reset();

        TimeSpan Elapsed { get; }
    }
}