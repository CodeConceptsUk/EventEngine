using System.Collections.Generic;

namespace Policy.Application.Interfaces
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