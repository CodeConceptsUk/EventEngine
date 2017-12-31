using System;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application.PropertyBags
{
    public class EventType : IEventType
    {
        public EventType(string type, Version version)
        {
            Type = type;
            Version = version;
        }

        public string Type { get; }

        public Version Version { get; }
    }
}