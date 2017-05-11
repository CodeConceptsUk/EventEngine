namespace Policy.Application.Interfaces.Factories
{
    public interface ICommandDispatcherFactory
    {
        ICommandDispatcher<TCommandBase> Create<TCommandBase, TEventBase>()
            where TCommandBase : class, ICommand
            where TEventBase : class, IEvent;
    }
}