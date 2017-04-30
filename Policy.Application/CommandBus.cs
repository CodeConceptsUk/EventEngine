﻿using System.Collections.Generic;
using System.Linq;
using FrameworkExtensions.LinqExtensions;
using FrameworkExtensions.ObjectExtensions;
using log4net;
using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;

namespace Policy.Application
{
    public class CommandBus : ICommandBus
    {
        private readonly IUnityContainer _container;
        private readonly IList<ICommandHandler> _handlers = new List<ICommandHandler>();
        private ILog _logger;

        public CommandBus(IUnityContainer container)
        {
            _logger = LogManager.GetLogger(typeof(CommandBus));
            var handlers = container.ResolveAll(typeof(ICommandHandler));
            handlers.ForEach(handler => _handlers.Add((ICommandHandler) handler));
            _container = container;
        }

        public void Apply(ICommand command)
        {
            var repository = _container.Resolve<IEventStoreRepository>();
            var handlers = GetHandler(command).ToArray();
            var events = new List<IEvent>();
            // TODO: Should we allow more than one handler per command?
            // TODO: Can one command run many actions to create events (for example create customer, create, email ...)
            _logger.Debug($"Applying {command.GetType().Name} using {handlers.Count()} handlers");
            handlers.ForEach(handler =>
            {
                _logger.Debug($"\tUsing handler {handler.GetType().Name}");
                // TODO: Pre (Can execute?)
                events.AddRange((IEnumerable<IEvent>) handler.Execute(command.AsDynamic()));
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