using System;
using EventEngine.Application.Interfaces.Commands;

namespace EventEngine.Application.Interfaces.Services
{
    public interface ICommandHandlerFilteringService
    {
        ICommandHandler[] Filter(ICommandHandler[] commandHandlers, Type commandType);
    }
}