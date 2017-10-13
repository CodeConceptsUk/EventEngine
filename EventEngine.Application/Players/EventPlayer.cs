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
                var eventEvaluators = GetEvaluators(@event.GetType(), typeof(TView));
                foreach (var eventEvaluator in eventEvaluators)
                {
                    var evaluateMethodInfo = eventEvaluator.GetType().GetMethod(nameof(IEventEvaluator<IEvent, TView>.Evaluate));
                    evaluateMethodInfo.Invoke(eventEvaluator, new object[] { view, @event });
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