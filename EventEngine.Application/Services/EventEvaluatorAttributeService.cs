using System;
using System.Linq;
using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces.Services;

namespace EventEngine.Application.Services
{
    public class EventEvaluatorAttributeService : IEventEvaluatorAttributeService
    {
        public (string EventName, Version MinimumVersion, Version MaximumVersion) Get(Type eventEvaluatorType)
        {
            var eventName = GetEventEvaluatorName(eventEvaluatorType);
            var minimumVersion = GetMinimumVersion(eventEvaluatorType);
            var maximumVersion = GetMaximumVersion(eventEvaluatorType);
            return (eventName, minimumVersion, maximumVersion);
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