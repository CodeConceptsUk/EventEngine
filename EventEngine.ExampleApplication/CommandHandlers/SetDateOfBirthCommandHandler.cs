using System;
using System.Collections.Generic;
using EventEngine.ExampleApplication.Commands;
using EventEngine.ExampleApplication.Events;
using EventEngine.Interfaces.Commands;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Factories;

namespace EventEngine.ExampleApplication.CommandHandlers
{
    public class SetDateOfBirthCommandHandler : ICommandHandler<SetDateOfBirthCommand>
    {
        private readonly IEventFactory _eventFactory;

        public SetDateOfBirthCommandHandler(IEventFactory eventFactory)
        {
            _eventFactory = eventFactory;
        }

        public IEnumerable<IEvent> Execute(Guid contextId, SetDateOfBirthCommand command)
        {
            var setNameEventData = new SetDateOfBirthEventData { DateOfBirth = command.DateOfBirth };
            var @event = _eventFactory.Create(contextId, setNameEventData);
            return new[] { @event };
        }
    }

    public class SetDateOfBirth2CommandHandler : ICommandHandler<SetDateOfBirth2Command>
    {
        private readonly IEventFactory _eventFactory;

        public SetDateOfBirth2CommandHandler(IEventFactory eventFactory)
        {
            _eventFactory = eventFactory;
        }

        public IEnumerable<IEvent> Execute(Guid contextId, SetDateOfBirth2Command command)
        {
            var setNameEventData = new SetDateOfBirthEventDataV2 { DateOfBirth = command.DateOfBirth, HourOfBirth = 10 };
            var @event = _eventFactory.Create(contextId, setNameEventData);
            return new[] { @event };
        }
    }
}