using System;

namespace CodeConcepts.FrameworkExtensions.Interfaces.Utilities
{
    public interface IStopwatch
    {
        void Start();

        void Stop();

        void Reset();

        TimeSpan Elapsed { get; }
    }
}