using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Interfaces;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Services;

namespace EventEngine.Services
{
    public class EventEvaluatorRegistry : IEventEvaluatorRegistry
    {
        private readonly IEventEvaluatorAttributeService _eventEvaluatorAttributeService;

        private class EventEvaluatorListItem {
            public string Name {get;set;}
            public Version MinimumVersion {get;set;}
            public Version MaximumVersion {get;set;}
            public Type ViewType {get;set;}
            public IEventEvaluator Evaluator {get;set;}
            public EventEvaluatorListItem(string name, Version minimumVersion, Version maximumVersion, Type viewType, IEventEvaluator evaluator)
            {
                Name = name;
                MinimumVersion = minimumVersion;
                MaximumVersion = maximumVersion;
                ViewType = viewType;
                Evaluator = evaluator;
            }
        }

        private readonly IList<EventEvaluatorListItem>
            _registeredEvaluators;

        public EventEvaluatorRegistry(IEventEvaluatorAttributeService eventEvaluatorAttributeService)
        {
            _eventEvaluatorAttributeService = eventEvaluatorAttributeService;
            _registeredEvaluators = new List<EventEvaluatorListItem>();
        }

        public void Register(params IEventEvaluator[] eventEvaluators)
        {
            foreach (var eventEvaluator in eventEvaluators)
            {
                var attributes = _eventEvaluatorAttributeService.Get(eventEvaluator.GetType());

                var viewTypes = eventEvaluator
                    .GetType()
                    .GetInterfaces()
                    .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEventEvaluator<,>))
                    .Select(t => t.GetGenericArguments()[0]).ToArray();

                foreach (var viewType in viewTypes)
                {
                    _registeredEvaluators.Add(new EventEvaluatorListItem(attributes.EventName, attributes.MinimumVersion, attributes.MaximumVersion, viewType, eventEvaluator));
                }
            }
        }

        public IEventEvaluator[] Filter<TView>(IEventType eventType)
            where TView : class, IView
        {
            return _registeredEvaluators
                .Where(registeredEvaluator =>
                    EvaluateEventView<TView>(registeredEvaluator.ViewType) &&
                    EvaluateEventName(eventType, registeredEvaluator.Name) &&
                    EvaluateVersionInRange(eventType, registeredEvaluator.MinimumVersion, registeredEvaluator.MaximumVersion))
                .Select(t => t.Evaluator)
                .ToArray();
        }

        private static bool EvaluateEventView<TView>(Type viewType)
        {
            return typeof(TView) == viewType;
        }

        private static bool EvaluateEventName(IEventType eventType, string eventName)
        {
            return eventType.Name.Equals(eventName);
        }

        private static bool EvaluateVersionInRange(IEventType eventType, Version minimumVersion, Version maximumVersion)
        {
            return eventType.Version >= minimumVersion &&
                   (maximumVersion == null || eventType.Version <= maximumVersion);
        }

        
    }
}
