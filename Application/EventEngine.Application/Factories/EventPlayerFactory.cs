using CodeConcepts.EventEngine.Application.Interfaces;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.FrameworkExtensions.Interfaces.Factories;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.Application.Factories
{
    public class EventPlayerFactory : IEventPlayerFactory
    {
        private readonly IUnityContainer _unityContainer;
        private readonly ILogFactory _logFactory;
        private readonly IStopwatchFactory _stopwatchFactory;

        public EventPlayerFactory(IUnityContainer unityContainer, ILogFactory logFactory, IStopwatchFactory stopwatchFactory)
        {
            _unityContainer = unityContainer;
            _logFactory = logFactory;
            _stopwatchFactory = stopwatchFactory;
        }

        public IEventPlayer<TEventBase> Create<TEventBase>()
            where TEventBase : class, IEvent
        {
            return new EventPlayer<TEventBase>(_unityContainer, _logFactory, _stopwatchFactory);
        }
    }
}