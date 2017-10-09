using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Repositories;

namespace EventEngine.Application.Interfaces.Factories
{
    public interface ICommandDispatcherFactory
    {
        ICommandDispatcher Create(IEventStore eventStore, params ICommandHandler[] commandHandlers);
    }
}