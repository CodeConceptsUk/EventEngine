using System.Collections.Generic;

namespace Policy.Application.Interfaces
{
    //TODO not happy that this is exposed in this namespace
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