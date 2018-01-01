using System.Collections.Generic;
using System.Linq;
using EventEngine.Application.Exceptions;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Repositories;
using EventEngine.Application.Interfaces.Services;

namespace EventEngine.Application.Dispatchers
{
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandHandlerFilteringService _commandHandlerFilteringService;
        private readonly ICommandHandler[] _commandHandlers;
        private readonly IEventStore _eventStore;

        internal CommandDispatcher(IEventStore eventStore, ICommandHandlerFilteringService commandHandlerFilteringService, params ICommandHandler[] commandHandlers)
        {
            _eventStore = eventStore;
            _commandHandlerFilteringService = commandHandlerFilteringService;
            _commandHandlers = commandHandlers;
        }

        public void Dispatch<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var events = new List<IEvent>();
            var handlers = _commandHandlerFilteringService.Filter(_commandHandlers, command.GetType());

            if (!handlers.Any())
                throw new EventEngineMissingCommandHandlerException(command);

            foreach (var handler in handlers)
            {
                var executionMethodInfo = handler.GetType().GetMethod(nameof(ICommandHandler<ICommand>.Execute));
                events.AddRange((IEnumerable<IEvent>) executionMethodInfo.Invoke(handler, new object[] {command}));
            }
            _eventStore.Add(events);
        }
    }
}