using CodeConcepts.FrameworkExtensions.Interfaces.Factories;
using CodeConcepts.FrameworkExtensions.Interfaces.Utilities;
using CodeConcepts.FrameworkExtensions.Utilities;

namespace CodeConcepts.FrameworkExtensions.Factories
{
    public class StopwatchFactory : IStopwatchFactory
    {
        public IStopwatch Create()
        {
            return new Stopwatch();
        }
    }
}
