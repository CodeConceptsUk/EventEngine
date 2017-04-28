using System;

namespace Policy.Application.Interfaces
{
    public interface IEvent<TContext>
        where TContext : class
    {
        Guid EventContextId { get; }

        Guid EventId { get; }

        DateTime EventDateTime { get; }
    }
}   