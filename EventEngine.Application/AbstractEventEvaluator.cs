using System;
using System.Linq;
using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;

namespace EventEngine.Application
{
    public abstract class AbstractEventEvaluator<TEventData, TView> : IEventEvaluator<TView, TEventData>
        where TEventData : IEventData
        where TView : class, IView
    {
        private readonly IEventDataDeserializationService _eventDataDeserializationService;

        protected AbstractEventEvaluator(IEventDataDeserializationService eventDataDeserializationService)
        {
            _eventDataDeserializationService = eventDataDeserializationService;
        }
        
        public void EvaluateGenericEvent(TView view, IEvent @event)
        {
            var eventData = _eventDataDeserializationService.Deserialize<TEventData>(@event.EventData);
            Evaluate(view, @event, eventData);
        }

        public abstract void Evaluate(TView view, IEvent @event, TEventData eventData);
    }
}