using EventEngine.Interfaces.Commands;

namespace EventEngine.Exceptions
{
    public class EventEngineMissingCommandHandlerException : EventEngineException
    {
        public EventEngineMissingCommandHandlerException(ICommand command)
            : base($"No event handler for command '{command.GetType()}'")
        {
            Command = command;
        }

        public ICommand Command { get; }
    }
}