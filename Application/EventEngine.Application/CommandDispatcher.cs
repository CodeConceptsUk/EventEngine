using System;
using System.Collections.Generic;
using System.Linq;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.FrameworkExtensions.LinqExtensions;
using CodeConcepts.FrameworkExtensions.ObjectExtensions;
using log4net;

namespace CodeConcepts.EventEngine.Application
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IEnumerable<ICommandHandler> _handlers;
        private readonly IEventRepositoryResolver _eventRepositoryResolver;
        private readonly ILog _logger;

        public CommandDispatcher(IEnumerable<ICommandHandler> handlers, ILogFactory logFactory, IEventRepositoryResolver eventRepositoryResolver)
        {
            _handlers = handlers;
            _eventRepositoryResolver = eventRepositoryResolver;
            _logger = logFactory.GetLogger(typeof(CommandDispatcher));
        }

        public void Apply(ICommand command)
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

            var results = (IEnumerable<IEvent>)handler.AsDynamic().Execute(command);

            if (results == null)
                throw new Exception($"Command {command.GetType().Name} returned null event list!");

            // TODO: Post (Can save?)

            var repository = _eventRepositoryResolver.Resolve(results.First().GetType());

            _logger.Debug($"\tAdding {results.Count()} event(s) to {repository.GetType().Name}");
            repository.Add(results);
        }

        private ICommandHandler GetHandler(ICommand command)
        {
            {
                return _handlers
                    .Single(t => t.GetType()
                                  .GetInterfaces()
                                  .Any(i => i.GetGenericArguments().Contains(command.GetType())));
            }
        }
    }
}