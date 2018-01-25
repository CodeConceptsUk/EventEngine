using System;
using EventEngine.Interfaces.Commands;

namespace EventEngine.Interfaces.Services
{
    public interface ICommandHandlerRegistry
    {
        void Register(params ICommandHandler[] commandHandlers);

        ICommandHandler[] Filter(Type commandType);
    }
}