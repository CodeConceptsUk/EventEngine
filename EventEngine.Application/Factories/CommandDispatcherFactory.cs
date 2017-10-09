using EventEngine.Application.Dispatchers;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.Application.Interfaces.Repositories;

namespace EventEngine.Application.Factories
{
    public class CommandDispatcherFactory : ICommandDispatcherFactory
    {
        public ICommandDispatcher Create(IEventStore eventStore, params ICommandHandler[] commandHandlers)
        {
            return new CommandDispatcher(eventStore, commandHandlers);
        }
    }
}