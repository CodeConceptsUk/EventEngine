using System.Collections.Generic;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application.Interfaces.Commands
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler
        where TCommand : class, ICommand
    {
        IEnumerable<IEvent> Execute(TCommand command);
    }
}