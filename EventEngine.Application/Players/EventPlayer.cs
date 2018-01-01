using System.Collections.Generic;
using System.Linq;
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
                {
                    EvaluateEvent(eventEvaluator, view, @event);
                }
            }
        }

        private void EvaluateEvent<TView>(IEventEvaluator eventEvaluator, TView view, IEvent @event)
            where TView : class, IView
        {
            var eventDataTypes = eventEvaluator
                .GetType()
                .GetInterfaces()
                .Where(t => t.IsGenericType &&
                            t.GetGenericTypeDefinition() == typeof(IEventEvaluator<,>) &&
                            t.GetGenericArguments()[0] == typeof(TView))
                .Select(t => t.GetGenericArguments()[1]).ToArray();

            foreach (var eventDataType in eventDataTypes)
            {
                var eventData = _eventDataDeserializationService.Deserialize(eventDataType, @event.EventData);
                ((dynamic) eventEvaluator).Evaluate(view, @event, (dynamic)eventData);
            }
        }

        
    }
}