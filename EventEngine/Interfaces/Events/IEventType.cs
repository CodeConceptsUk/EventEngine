using System;

namespace EventEngine.Interfaces.Events
{
    public interface IEventType
    {
        string Name { get; }

        Version Version { get; }
    }
}