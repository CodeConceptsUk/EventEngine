using System.Collections.Generic;
using System.Linq;
using EventEngine.Application.Exceptions;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Repositories;
using EventEngine.Application.Interfaces.Services;

namespace EventEngine.Application.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandHandlerRegistry _commandHandlerRegistry;
        private readonly IEventStore _eventStore;

        public CommandDispatcher(IEventStore eventStore, ICommandHandlerRegistry commandHandlerRegistry)
        {
            _eventStore = eventStore;
            _commandHandlerRegistry = commandHandlerRegistry;
        }

        public void Dispatch<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var events = new List<IEvent>();
            var handlers = _commandHandlerRegistry.Filter(command.GetType());

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