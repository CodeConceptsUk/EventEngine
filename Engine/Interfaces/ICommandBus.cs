using Engine.Commands;

namespace Engine.Interfaces
{
    public interface ICommandBus
    {
        void Publish<TCommand>(TCommand command)
            where TCommand : ICommand;
    }

    public interface IEventStateMachine
    {
        void Apply<TEvent>(TEvent @event)
            where TEvent : IEvent;
    }
}