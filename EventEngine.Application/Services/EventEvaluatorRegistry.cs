using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Application.Interfaces;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;

namespace EventEngine.Application.Services
{
    public class EventEvaluatorRegistry : IEventEvaluatorRegistry
    {
        private readonly IEventEvaluatorAttributeService _eventEvaluatorAttributeService;

        private readonly IList<(string Name, Version MinimumVersion, Version MaximumVersion, Type ViewType, IEventEvaluator Evaluator)>
            _registeredEvaluators;

        public EventEvaluatorRegistry(IEventEvaluatorAttributeService eventEvaluatorAttributeService)
        {
            _eventEvaluatorAttributeService = eventEvaluatorAttributeService;
            _registeredEvaluators = new List<(string Name, Version MinimumVersion, Version MaximumVersion, Type ViewType, IEventEvaluator Evaluator)>();
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
                    _registeredEvaluators.Add((attributes.EventName, attributes.MinimumVersion, attributes.MaximumVersion, viewType, eventEvaluator));
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