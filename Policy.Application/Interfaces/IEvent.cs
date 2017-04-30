using System;

namespace Policy.Application.Interfaces
{
    public interface IEvent
    {
        Guid EventContextId { get; }

        Guid EventId { get; }

        DateTime EventDateTime { get; }
    }
}   