using System;
using EventEngine.Interfaces.Commands;

namespace EventEngine.Interfaces.Services
{
    public interface ICommandHandlerRegistry
    {
        ICommandHandler[] Filter(Type commandType);
    }
}