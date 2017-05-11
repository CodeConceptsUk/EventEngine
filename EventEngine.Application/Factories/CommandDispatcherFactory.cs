using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Factories;

namespace Policy.Application.Factories
{
    public class CommandDispatcherFactory : ICommandDispatcherFactory
    {
        private readonly IUnityContainer _unityContainer;
        private readonly ILogFactory _logFactory;

        public CommandDispatcherFactory(IUnityContainer unityContainer, ILogFactory logFactory)
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