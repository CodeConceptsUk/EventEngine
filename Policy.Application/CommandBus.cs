using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Policy.Application.Extensions;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;

namespace Policy.Application
{
    public class CommandBus : ICommandBus
    {
        private readonly IUnityContainer _container;
        private readonly IList<ICommandHandler> _handlers = new List<ICommandHandler>();

        public CommandBus(IUnityContainer container)
        {
            var handlers = container.ResolveAll(typeof(ICommandHandler));
            handlers.ForEach(handler => _handlers.Add((ICommandHandler) handler));
            _container = container;
        }

        public void Apply<TContext>(ICommand<TContext> command)
            where TContext : class, IContext
        {
            var repository = _container.Resolve<IEventStoreRepository<TContext>>();
            var handlers = GetHandler(command);

            // TODO: Should we allow more than one handler per command?
            // TODO: Can one command run many actions to create events (for example create customer, create, email ...)

            handlers.ForEach(handler =>
            {
                // TODO: Pre (Can execute?)
                var @event = (IEnumerable<IEvent<TContext>>) handler.Execute(command.AsDynamic());
                // TODO: Post (Can save?)
                repository.Add(@event);
            });
        }

        private IEnumerable<dynamic> GetHandler<TContext>(ICommand<TContext> command)
            where TContext : class, IContext
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