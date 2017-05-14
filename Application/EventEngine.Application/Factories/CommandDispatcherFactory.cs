using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
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
            return new CommandDispatcher<TCommandBase, TEventBase>(_unityContainer, _logFactory);
        }
    }
}