using System;

namespace EventEngine.Interfaces.Commands
{
    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(Guid contextId, TCommand command)
            where TCommand : ICommand;
    }
}