using System;
using EventEngine.Interfaces.Events;

namespace EventEngine.PropertyBags
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