using EventEngine.Application.Interfaces.Commands;

namespace EventEngine.Application.Exceptions
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