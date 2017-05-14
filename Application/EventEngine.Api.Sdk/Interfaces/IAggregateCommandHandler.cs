using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.Contracts.Interfaces
{
    public interface IAggregateCommandHandler : ICommandHandler
    {
    }

    public interface IAggregateCommandHandler<in TCommand, out TEvent> : IAggregateCommandHandler
        where TCommand : class, ICommand
        where TEvent : class, IEvent
    {
        IEnumerable<ICommand> Execute(TCommand command);
    }
}