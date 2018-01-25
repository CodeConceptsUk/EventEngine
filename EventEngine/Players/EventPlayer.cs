using System.Collections.Generic;
using System.Linq;
using EventEngine.Interfaces;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Services;

namespace EventEngine.Players
{
    public class EventPlayer : IEventPlayer
    {
        private readonly IEventEvaluatorRegistry _eventEvaluatorRegistry;
        private readonly IEventDataDeserializationService _eventDataDeserializationService;

        public EventPlayer(IEventEvaluatorRegistry eventEvaluatorRegistry,
            IEventDataDeserializationService eventDataDeserializationService)
        {
            _eventEvaluatorRegistry = eventEvaluatorRegistry;
            _eventDataDeserializationService = eventDataDeserializationService;
        }

        public void Play<TView>(IEnumerable<IEvent> events, TView view)
            where TView : class, IView
        {
            foreach (var @event in events)
            {
                var eventEvaluators = _eventEvaluatorRegistry.Filter<TView>(@event.EventType);
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