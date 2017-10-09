using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Application.Exceptions;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Repositories;

namespace EventEngine.Application.Dispatchers
{
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly IEventStore _eventStore;
        private readonly ICommandHandler[] _commandHandlers;

        internal CommandDispatcher(IEventStore eventStore, params ICommandHandler[] commandHandlers)
        {
            _eventStore = eventStore;
            _commandHandlers = commandHandlers;
        }

        public void Dispatch<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var events = new List<IEvent>();
            var handlers = GetHandlers(command.GetType()).ToArray();

            if (!handlers.Any())
                throw new EventEngineMissingCommandHandlerException(command);

            foreach (var handler in handlers)
            {
                var executionMethodInfo = handler.GetType().GetMethod(nameof(ICommandHandler<ICommand>.Execute));
                events.AddRange((IEnumerable<IEvent>)executionMethodInfo.Invoke(handler, new object[] { command }));
            }
            _eventStore.Add(events);
        }

        public IEnumerable<ICommandHandler> GetHandlers(Type command)
        {
            return _commandHandlers.Where(t => t.GetType()
                .GetInterfaces()
                .Any(i => i.GetGenericArguments().Contains(command)));
        }
    }
}