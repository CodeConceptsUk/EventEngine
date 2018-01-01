using EventEngine.Application.Dispatchers;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.Application.Interfaces.Repositories;
using EventEngine.Application.Interfaces.Services;

namespace EventEngine.Application.Factories
{
    public class CommandDispatcherFactory : ICommandDispatcherFactory
    {
        private readonly ICommandHandlerFilteringService _commandHandlerFilteringService;
        private readonly IEventStore _eventStore;

        public CommandDispatcherFactory(IEventStore eventStore, ICommandHandlerFilteringService commandHandlerFilteringService)
        {
            _eventStore = eventStore;
            _commandHandlerFilteringService = commandHandlerFilteringService;
        }

        public ICommandDispatcher Create(ICommandHandler[] commandHandlers)
        {
            return new CommandDispatcher(_eventStore, _commandHandlerFilteringService, commandHandlers);
        }
    }
}