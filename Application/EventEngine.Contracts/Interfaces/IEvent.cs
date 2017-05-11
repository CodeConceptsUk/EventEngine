using System;

namespace CodeConcepts.EventEngine.Contracts.Interfaces
{
    public interface IEvent
    {
        Guid EventContextId { get; }

        Guid EventId { get; }

        DateTime EventDateTime { get; }
    }
}   