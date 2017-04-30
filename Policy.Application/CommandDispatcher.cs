using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FrameworkExtensions.LinqExtensions;
using FrameworkExtensions.ObjectExtensions;
using log4net;
using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;

namespace Policy.Application
{
    public class CommandDispatcher<TCommand, TEvent> : ICommandDispatcher<TCommand>
        where TCommand : class, ICommand
        where TEvent : class, IEvent
    {
        private readonly IUnityContainer _container;
        private readonly IList<ICommandHandler> _handlers = new List<ICommandHandler>();
        private readonly ILog _logger;

        public CommandDispatcher(IUnityContainer container)
        {
            _logger = LogManager.GetLogger(typeof(CommandDispatcher<TCommand, TEvent>));
            var handlers = container.ResolveAll(typeof(ICommandHandler));
            handlers.ForEach(handler => _handlers.Add((ICommandHandler)handler));
            _container = container;
        }

        public void Apply(TCommand command)
        {
            var repository = _container.Resolve<IEventStoreRepository<TEvent>>(); // TODO: Add type for Event
            var handlers = GetHandler(command).ToArray();
            var events = new List<TEvent>();
            // TODO: Should we allow more than one handler per command?
            // TODO: Can one command run many actions to create events (for example create customer, create, email ...)
            _logger.Debug($"Applying {command.GetType().Name} using {handlers.Count()} handlers");
            handlers.ForEach(handler =>
            {
                _logger.Debug($"\tUsing handler {handler.GetType().Name}");
                // TODO: Pre (Can execute?)
                var results = handler.Execute(command.AsDynamic()) as IEnumerable;
                events.AddRange(results.OfType<TEvent>());
            });
            _logger.Debug($"\tAdding {events.Count} event(s) to {repository.GetType().Name}");
            // TODO: Post (Can save?)
            repository.Add(events);
        }

        private IEnumerable<dynamic> GetHandler(ICommand command)
        {
            {
                return _handlers.Where(t => t.GetType()
                    .GetInterfaces()
                    .Any(i => i.GetGenericArguments().Contains(command.GetType())))
                    .Select(t => t.AsDynamic());
            }
        }
    }
}