using System.Collections.Generic;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.ExampleApplication.Commands;
using EventEngine.ExampleApplication.Events;

namespace EventEngine.ExampleApplication.CommandHandlers
{
    public class SetDateOfBirthCommandHandler : ICommandHandler<SetDateOfBirthCommand>
    {
        private readonly IEventFactory _eventFactory;

        public SetDateOfBirthCommandHandler(IEventFactory eventFactory)
        {
            _eventFactory = eventFactory;
        }

        public IEnumerable<IEvent> Execute(SetDateOfBirthCommand command)
        {
            var setNameEventData = new SetDateOfBirthEventData {DateOfBirth = command.DateOfBirth};
            var @event = _eventFactory.Create(command.ContextId, setNameEventData);
            return new[] {@event};
        }
    }
}