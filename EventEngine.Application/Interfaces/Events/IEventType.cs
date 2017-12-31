using System;

namespace EventEngine.Application.Interfaces.Events
{
    public interface IEventType
    {
        string Type { get; }

        Version Version { get; }
    }
}