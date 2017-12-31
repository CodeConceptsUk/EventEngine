using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EventEngine.Application.Attributes;
using EventEngine.Application.Dispatchers;
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
                    var evaluateMethodInfo = eventEvaluator.GetType().GetMethod(nameof(IEventEvaluator<TView>.EvaluateGenericEvent));
                    evaluateMethodInfo.Invoke(eventEvaluator, new object[] { view, @event });
                }
            }
            return view;
        }

        private IEventEvaluator[] GetEvaluators(Type eventType, Type viewType)
        {
            var eventName = (EventNameAttribute)eventType.GetCustomAttributes(typeof(EventNameAttribute), true)[0];
            var eventTarget = (VersionAttribute)eventType.GetCustomAttributes(typeof(VersionAttribute), true)
                .FirstOrDefault() ?? new VersionAttribute();

            return _eventEvaluators
                .Where(e => e.GetType()
                    .GetInterfaces()
                    .Any(i => i.IsGenericType &&
                              i.GetGenericArguments().Length == 1 &&
                              i.GetGenericArguments()[0] == viewType)
                           && (e.Name.Equals(eventName.Name) && eventTarget.Version >= e.MinimumVersion && 
                              (e.MaximumVersion == null || eventTarget.Version <= e.MaximumVersion)))
                .ToArray();
        }
    }
}