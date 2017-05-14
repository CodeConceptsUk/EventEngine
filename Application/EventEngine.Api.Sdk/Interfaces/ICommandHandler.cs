using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.Contracts.Interfaces
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<in TCommand, out TEvent> : ICommandHandler
        where TCommand : class, ICommand
        where TEvent : class, IEvent
    {
        IEnumerable<TEvent> Execute(TCommand command);
    }
}
