using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.FrameworkExtensions.Interfaces.Factories;
using SimpleInjector;

namespace CodeConcepts.EventEngine.Application.Factories
{
    public class EventPlayerFactory : IEventPlayerFactory
    {
        private readonly Container _unityContainer;
        private readonly ILogFactory _logFactory;
        private readonly IStopwatchFactory _stopwatchFactory;

        public EventPlayerFactory(Container unityContainer, ILogFactory logFactory, IStopwatchFactory stopwatchFactory)
        {
            _unityContainer = unityContainer;
            _logFactory = logFactory;
            _stopwatchFactory = stopwatchFactory;
        }

        public IEventPlayer<TEventBase> Create<TEventBase>()
            where TEventBase : class, IEvent
        {
            var handlers = _unityContainer.GetAllInstances<IEventEvaluator>();
            return new EventPlayer<TEventBase>(handlers, _logFactory, _stopwatchFactory);
        }
    }
}