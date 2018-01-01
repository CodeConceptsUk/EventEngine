using System;
using System.Linq;
using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;

namespace EventEngine.Application.Services
{
    public class EventEvaluatorFilteringService : IEventEvaluatorFilteringService
    {
        public IEventEvaluator[] Filter<TView>(IEventEvaluator[] eventEvaluators, IEventType eventType)
            where TView : class, IView
        {
            return eventEvaluators
                .Where(e => e.GetType()
                                .GetInterfaces()
                                .Any(i => i == typeof(IEventEvaluator<TView>))
                            && e.Name.Equals(eventType.Type) 
                            && eventType.Version >= e.MinimumVersion 
                            && (e.MaximumVersion == null 
                                || eventType.Version <= e.MaximumVersion))
                .ToArray();
        }
    }
}