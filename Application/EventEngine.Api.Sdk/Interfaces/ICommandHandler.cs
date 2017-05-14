using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.Contracts.Interfaces
{
    public interface ICommandHandler
    {
    }

    //TODO why is this generic?
    public interface ICommandHandler<in TCommand> : ICommandHandler
        where TCommand : class, ICommand
    {
        IEnumerable<IEvent> Execute(TCommand command);
    }
}
