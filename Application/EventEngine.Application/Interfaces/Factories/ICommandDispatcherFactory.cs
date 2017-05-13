using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.Application.Interfaces.Factories
{
    public interface ICommandDispatcherFactory
    {
        ICommandDispatcher<TCommandBase> Create<TCommandBase, TEventBase>()
            where TCommandBase : class, ICommand
            where TEventBase : class, IEvent;
    }
}