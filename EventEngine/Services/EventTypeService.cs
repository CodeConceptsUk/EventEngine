using System.Linq;
using EventEngine.Application.Attributes;
using EventEngine.Application.Exceptions;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;
using EventEngine.Application.PropertyBags;

namespace EventEngine.Application.Services
{
    public class EventTypeService : IEventTypeService
    {
        public IEventType Get<TEventData>() where TEventData : IEventData
        {
            var eventName = (EventNameAttribute) typeof(TEventData).GetCustomAttributes(typeof(EventNameAttribute), true).SingleOrDefault();
            if (string.IsNullOrWhiteSpace(eventName?.Name))
                throw new EventDeclarationException($"Event '{typeof(TEventData).Name}' is missing the EventName attribute or the attribute has no value!");
            var eventTarget = (VersionAttribute) typeof(TEventData).GetCustomAttributes(typeof(VersionAttribute), true).SingleOrDefault() ?? new VersionAttribute();
            return new EventType(eventName.Name, eventTarget.Version);
        }
    }
}