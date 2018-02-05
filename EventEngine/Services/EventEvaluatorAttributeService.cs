using System;
using System.Linq;
using EventEngine.Attributes;
using EventEngine.Interfaces.Services;
using EventEngine.PropertyBags;

namespace EventEngine.Services
{
    public class EventEvaluatorAttributeService : IEventEvaluatorAttributeService
    {
        public EventValidityData Get(Type eventEvaluatorType)
        {
            var eventName = GetEventEvaluatorName(eventEvaluatorType);
            var minimumVersion = GetMinimumVersion(eventEvaluatorType);
            var maximumVersion = GetMaximumVersion(eventEvaluatorType);
            return new EventValidityData
            {
                EventName = eventName,
                MinimumVersion = minimumVersion,
                MaximumVersion = maximumVersion
            };
        }

        private static string GetEventEvaluatorName(Type eventEvaluatorType)
        {
            var attribute = (EventNameAttribute)eventEvaluatorType
                .GetCustomAttributes(typeof(EventNameAttribute), true)
                .Single();
            return attribute.Name;
        }

        private static Version GetMinimumVersion(Type eventEvaluatorType)
        {
            var attribute = (MinimumVersionAttribute)eventEvaluatorType
                                .GetCustomAttributes(typeof(MinimumVersionAttribute), true)
                                .SingleOrDefault() ?? new MinimumVersionAttribute();
            return attribute.Version;
        }

        private static Version GetMaximumVersion(Type eventEvaluatorType)
        {
            var attribute = (MaximumVersionAttribute)eventEvaluatorType
                .GetCustomAttributes(typeof(MaximumVersionAttribute), true)
                .SingleOrDefault();
            return attribute?.Version;
        }
    }
}