using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using SimpleInjector;

namespace CodeConcepts.EventEngine.Application.Factories
{
    public class CommandDispatcherFactory : ICommandDispatcherFactory
    {
        private readonly Container _unityContainer;
        private readonly ILogFactory _logFactory;

        public CommandDispatcherFactory(Container unityContainer, ILogFactory logFactory)
        {
            _unityContainer = unityContainer;
            _logFactory = logFactory;
        }

        public ICommandDispatcher<TCommandBase> Create<TCommandBase, TEventBase>()
            where TCommandBase : class, ICommand
            where TEventBase : class, IEvent
        {
            var repo = _unityContainer.GetInstance<IEventStoreRepository<TEventBase>>();
            var commandHandlers = _unityContainer.GetAllInstances<ICommandHandler>();
            return new CommandDispatcher<TCommandBase, TEventBase>(commandHandlers, repo, _logFactory);
        }
    }
}