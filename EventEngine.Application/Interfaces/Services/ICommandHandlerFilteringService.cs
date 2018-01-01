using System;
using System.Collections.Generic;
using EventEngine.Application.Interfaces.Commands;

namespace EventEngine.Application.Interfaces.Services
{
    public interface ICommandHandlerFilteringService
    {
        ICommandHandler[] Filter(ICommandHandler[] commandHandlers, Type commandType);
    }
}