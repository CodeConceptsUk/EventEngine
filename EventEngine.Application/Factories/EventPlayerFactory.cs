using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.Application.Interfaces.Services;
using EventEngine.Application.Players;

namespace EventEngine.Application.Factories
{
    public class EventPlayerFactory : IEventPlayerFactory
    {
        private readonly IEventEvaluatorFilteringService _eventEvaluatorFilteringService;

        public EventPlayerFactory(IEventEvaluatorFilteringService eventEvaluatorFilteringService)
        {
            _eventEvaluatorFilteringService = eventEvaluatorFilteringService;
        }

        public IEventPlayer Create(params IEventEvaluator[] eventEvaluators)
        {
            return new EventPlayer(_eventEvaluatorFilteringService, eventEvaluators);
        }
    }
}