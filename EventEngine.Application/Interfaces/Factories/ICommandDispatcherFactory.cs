using EventEngine.Application.Interfaces.Commands;

namespace EventEngine.Application.Interfaces.Factories
{
    public interface ICommandDispatcherFactory
    {
        ICommandDispatcher Create(ICommandHandler[] commandHandlers);
    }
}