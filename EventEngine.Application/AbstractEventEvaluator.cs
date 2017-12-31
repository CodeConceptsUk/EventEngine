using System;
using System.Linq;
using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application
{
    public abstract class AbstractEventEvaluator<TEventData, TView> : IEventEvaluator<TView>
       where TEventData : IEventData
       where TView : class, IView
    {
        protected AbstractEventEvaluator()
        {
            MinimumVersion = GetMinimumVersion().Version;
            MaximumVersion = GetMaximumVersion()?.Version;
            Name = GetName().Name;
        }

        public string Name { get; }

        public Version MinimumVersion { get; }

        public Version MaximumVersion { get; }

        public abstract void Evaluate(TView view, IEvent @event, TEventData eventData);

        public void EvaluateGenericEvent(TView view, IEvent @event)
        {
            
        }

        private EventNameAttribute GetName()
        {
            var eventName = (EventNameAttribute)GetType()
                .GetCustomAttributes(typeof(EventNameAttribute), true)
                .Single();
            return eventName;
        }

        private MinimumVersionAttribute GetMinimumVersion()
        {
            var minimumVersion = (MinimumVersionAttribute)GetType()
                .GetCustomAttributes(typeof(MinimumVersionAttribute), true)
                .SingleOrDefault() ?? new MinimumVersionAttribute();
            return minimumVersion;
        }

        private MaximumVersionAttribute GetMaximumVersion()
        {
            var minimumVersion = (MaximumVersionAttribute)GetType()
                .GetCustomAttributes(typeof(MaximumVersionAttribute), true)
                .SingleOrDefault();
            return minimumVersion;
        }
    }
}