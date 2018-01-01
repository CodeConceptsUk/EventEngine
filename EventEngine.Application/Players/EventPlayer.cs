using System.Collections.Generic;
using EventEngine.Application.Interfaces;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;

namespace EventEngine.Application.Players
{
    public class EventPlayer : IEventPlayer
    {
        private readonly IEventEvaluatorFilteringService _eventEvaluatorFilteringService;
        private readonly IEventDataDeserializationService _eventDataDeserializationService;

        public EventPlayer(IEventEvaluatorFilteringService eventEvaluatorFilteringService,
            IEventDataDeserializationService eventDataDeserializationService)
        {
            _eventEvaluatorFilteringService = eventEvaluatorFilteringService;
            _eventDataDeserializationService = eventDataDeserializationService;
        }

        public void Play<TView>(IEnumerable<IEvent> events, TView view)
            where TView : class, IView
        {
            foreach (var @event in events)
            {
                var eventEvaluators = _eventEvaluatorFilteringService.Filter<TView>(@event.EventType);
                foreach (var eventEvaluator in eventEvaluators)
                    ((dynamic)eventEvaluator).EvaluateGenericEvent(view, @event);
            }
        }
    }
}