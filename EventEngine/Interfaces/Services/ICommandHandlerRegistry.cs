using System;
using EventEngine.Application.Interfaces.Commands;

namespace EventEngine.Application.Interfaces.Services
{
    public interface ICommandHandlerRegistry
    {
        void Register(params ICommandHandler[] commandHandlers);

        ICommandHandler[] Filter(Type commandType);
    }
}