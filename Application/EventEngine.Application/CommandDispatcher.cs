using System;
using System.Collections.Generic;
using System.Linq;
using CodeConcepts.EventEngine.Application.Interfaces;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.FrameworkExtensions.LinqExtensions;
using CodeConcepts.FrameworkExtensions.ObjectExtensions;
using log4net;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.Application
{
    public class CommandDispatcher<TCommand, TEvent> : ICommandDispatcher<TCommand>
        where TCommand : class, ICommand
        where TEvent : class, IEvent
    {
        private readonly IUnityContainer _container;
        private readonly IList<ICommandHandler> _handlers = new List<ICommandHandler>();
        private readonly ILog _logger;

        public CommandDispatcher(IUnityContainer container, ILogFactory logFactory)
        {
            _logger = logFactory.GetLogger(typeof(CommandDispatcher<,>));
            var handlers = container.ResolveAll(typeof(ICommandHandler));
            handlers.ForEach(handler => _handlers.Add((ICommandHandler)handler));
            _container = container;
        }

        public void Apply(TCommand command)
        {
            var repository = _container.Resolve<IEventStoreRepository<TEvent>>(); // TODO: Add type for Event
            var handler = GetHandler(command);

            _logger.Debug($"Applying {command.GetType().Name} using {handler?.GetType()?.Name}");

            // TODO: Pre (Can execute?)

            var results = (IEnumerable<TEvent>) handler.Execute(command.AsDynamic());

            if (results == null)
            {
                throw new Exception($"Command {command.GetType().Name} returned null event list!");
            }

            // TODO: Post (Can save?)

            _logger.Debug($"\tAdding {results.Count()} event(s) to {repository.GetType().Name}");
            repository.Add(results);
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