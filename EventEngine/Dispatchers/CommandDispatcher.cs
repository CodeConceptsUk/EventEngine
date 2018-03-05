using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Exceptions;
using EventEngine.Interfaces.Commands;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Repositories;
using EventEngine.Interfaces.Services;

namespace EventEngine.Dispatchers
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

        public void Dispatch<TCommand>(Guid contextId, TCommand command)
            where TCommand : ICommand
        {
            var events = new List<IEvent>();
            var handlers = _commandHandlerRegistry.Filter(command.GetType());

            if (!handlers.Any())
                throw new EventEngineMissingCommandHandlerException(command);

            foreach (var handler in handlers)
            {
                var executionMethodInfo = handler.GetType().GetMethod(nameof(ICommandHandler<ICommand>.Execute));
                events.AddRange((IEnumerable<IEvent>) executionMethodInfo.Invoke(handler, new object[] { contextId, command }));
            }
            _eventStore.Add(events);
        }
    }
}