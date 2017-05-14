using System;

namespace CodeConcepts.EventEngine.Api.Contracts
{
    public interface IEventContextIdQuery : IQuery
    {
        Guid EventContextId { get; set; }
    }
}