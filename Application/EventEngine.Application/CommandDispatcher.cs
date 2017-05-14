using System;
using System.Collections.Generic;
using System.Linq;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.FrameworkExtensions.ObjectExtensions;
using log4net;

namespace CodeConcepts.EventEngine.Application
{
    public class CommandDispatcher<TCommand, TEvent> : ICommandDispatcher<TCommand>
        where TCommand : class, ICommand
        where TEvent : class, IEvent
    {
        private readonly IEnumerable<ICommandHandler> _handlers;
        private readonly ILog _logger;
        private readonly IEventStoreRepository<TEvent> _repository;

        public CommandDispatcher(IEnumerable<ICommandHandler> handlers, IEventStoreRepository<TEvent> repository, ILogFactory logFactory)
        {
            _handlers = handlers;
            _repository = repository;
            _logger = logFactory.GetLogger(typeof(CommandDispatcher<,>));
        }

        public void Apply(TCommand command)
        {
            var handler = GetHandler(command);
            var handlerType = handler.GetType();

            if (typeof(IAggregateCommandHandler).IsAssignableFrom(handlerType))
            {
                var results = (IEnumerable<ICommand>)handler.Execute(command.AsDynamic());
                results.ForEach(ExecuteHandler);
                return;
            }

            ExecuteHandler(command);
        }

        private void ExecuteHandler(ICommand command)
        {
            var handler = GetHandler(command);

            _logger.Debug($"Applying {command.GetType().Name} using {handler?.GetType()?.Name}");

            // TODO: Pre (Can execute?)

            var results = (IEnumerable<TEvent>)handler.Execute(command.AsDynamic());

            if (results == null)
                throw new Exception($"Command {command.GetType().Name} returned null event list!");

            // TODO: Post (Can save?)

            _logger.Debug($"\tAdding {results.Count()} event(s) to {_repository.GetType().Name}");
            _repository.Add(results);
        }

        private dynamic GetHandler(ICommand command)
        {
            {
                return _handlers.Where(t => t.GetType()
                    .GetInterfaces()
                    .Any(i => i.GetGenericArguments().Contains(command.GetType())))
                    .Select(t => t.AsDynamic()).Single();
            }
        }
    }
}