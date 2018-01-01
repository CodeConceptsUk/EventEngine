using System;
using System.Linq;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Services;

namespace EventEngine.Application.Services
{
    public class CommandHandlerFilteringService : ICommandHandlerFilteringService
    {
        public ICommandHandler[] Filter(ICommandHandler[] commandHandlers, Type commandType)
        {
            return commandHandlers.Where(t => t.GetType()
                    .GetInterfaces()
                    .Any(i => i.GetGenericArguments().Contains(commandType)))
                .ToArray();
        }
    }
}