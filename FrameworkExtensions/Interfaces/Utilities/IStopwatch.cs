using System;
using System.Threading;

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