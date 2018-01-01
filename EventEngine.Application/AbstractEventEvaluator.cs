using System;
using System.Linq;
using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;

namespace EventEngine.Application
{
    public abstract class AbstractEventEvaluator<TEventData, TView> : IEventEvaluator<TView>
        where TEventData : IEventData
        where TView : class, IView
    {
        private readonly IEventDataDeserializationService _eventDataDeserializationService;

        protected AbstractEventEvaluator(IEventDataDeserializationService eventDataDeserializationService)
        {
            _eventDataDeserializationService = eventDataDeserializationService;
            MinimumVersion = GetMinimumVersion().Version;
            MaximumVersion = GetMaximumVersion()?.Version;
            Name = GetName().Name;
        }

        public string Name { get; }

        public Version MinimumVersion { get; }

        public Version MaximumVersion { get; }

        public void EvaluateGenericEvent(TView view, IEvent @event)
        {
            var eventData = _eventDataDeserializationService.Deserialize<TEventData>(@event.EventData);
            Evaluate(view, @event, eventData);
        }

        public abstract void Evaluate(TView view, IEvent @event, TEventData eventData);

        private EventNameAttribute GetName()
        {
            var attribute = (EventNameAttribute) GetType()
                .GetCustomAttributes(typeof(EventNameAttribute), true)
                .Single();
            return attribute;
        }

        private MinimumVersionAttribute GetMinimumVersion()
        {
            var attribute = (MinimumVersionAttribute) GetType()
                                .GetCustomAttributes(typeof(MinimumVersionAttribute), true)
                                .SingleOrDefault() ?? new MinimumVersionAttribute();
            return attribute;
        }

        private MaximumVersionAttribute GetMaximumVersion()
        {
            var attribute = (MaximumVersionAttribute) GetType()
                .GetCustomAttributes(typeof(MaximumVersionAttribute), true)
                .SingleOrDefault();
            return attribute;
        }
    }
}