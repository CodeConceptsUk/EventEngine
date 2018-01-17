using System;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application.PropertyBags
{
    public class EventType : IEventType
    {
        public EventType(string type, Version version)
        {
            Name = type;
            Version = version;
        }

        public string Name { get; }

        public Version Version { get; }
    }
}