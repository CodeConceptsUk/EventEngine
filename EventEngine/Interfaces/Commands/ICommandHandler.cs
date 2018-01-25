using System.Collections.Generic;
using EventEngine.Interfaces.Events;

namespace EventEngine.Interfaces.Commands
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