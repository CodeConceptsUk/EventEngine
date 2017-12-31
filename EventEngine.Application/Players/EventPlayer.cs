using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Application.Interfaces;
using EventEngine.Application.Interfaces.Events;

// ReSharper disable ReturnTypeCanBeEnumerable.Local

namespace EventEngine.Application.Players
{
    internal class EventPlayer : IEventPlayer
    {
        private readonly IEventEvaluator[] _eventEvaluators;

        internal EventPlayer(params IEventEvaluator[] eventEvaluators)
        {
            _eventEvaluators = eventEvaluators;
        }

        public TView Evaluate<TView, TEvent>(IEnumerable<TEvent> events, TView view)
            where TView : class, IView
            where TEvent : class, IEvent
        {
            foreach (var @event in events)
            {
                var eventType = @event.EventData.GetType();
                var eventEvaluators = GetEvaluators(eventType, typeof(TView));
                foreach (var eventEvaluator in eventEvaluators)
                {
                    var evaluateMethodInfo = eventEvaluator.GetType().GetMethod(nameof(IEventEvaluator<IEventData, TView>.Evaluate));
                    evaluateMethodInfo.Invoke(eventEvaluator, new object[] { view, (dynamic)@event });
                }
            }
            return view;
        }
        
        private IEventEvaluator[] GetEvaluators(Type eventType, Type viewType)
        {
            return _eventEvaluators
                .Where(e => e.GetType()
                    .GetInterfaces()
                    .Any(i => i.IsGenericType &&
                              i.GetGenericArguments().Length == 2 &&
                              i.GetGenericArguments()[0] == eventType &&
                              i.GetGenericArguments()[1] == viewType))
                .ToArray();
        }
    }
}