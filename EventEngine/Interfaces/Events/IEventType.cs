using System;

namespace EventEngine.Application.Interfaces.Events
{
    public interface IEventType
    {
        string Name { get; }

        Version Version { get; }
    }
}