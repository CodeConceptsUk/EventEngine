using System;

namespace Application.Interfaces
{
    public interface IEvent<TContext>
        where TContext : class
    {
        Guid EventContextId { get; set; }

        Guid EventId { get; set; }

        DateTime EventDateTime { get; set; }
    }
}   