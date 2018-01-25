using System.Linq;
using EventEngine.Attributes;
using EventEngine.Exceptions;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Services;
using EventEngine.PropertyBags;

namespace EventEngine.Services
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