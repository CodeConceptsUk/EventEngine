using FrameworkExtensions.Interfaces.Factories;
using FrameworkExtensions.Interfaces.Utilities;
using FrameworkExtensions.Utilities;

namespace FrameworkExtensions.Factories
{
    public class StopwatchFactory : IStopwatchFactory
    {
        public IStopwatch Create()
        {
            return new Stopwatch();
        }
    }
}
