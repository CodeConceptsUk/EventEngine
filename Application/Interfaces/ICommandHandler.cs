using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<in TCommand, TContext> : ICommandHandler
        where TCommand : class, ICommand<TContext>
        where TContext : class, IContext
    {
        IEnumerable<IEvent<TContext>> Execute(TCommand command);
    }
}