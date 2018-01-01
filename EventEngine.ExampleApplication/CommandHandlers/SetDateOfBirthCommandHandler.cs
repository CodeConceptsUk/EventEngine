using System;
using System.Collections.Generic;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.ExampleApplication.Commands;
using EventEngine.ExampleApplication.Events;
using EventEngine.Application.Attributes;

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
            var setNameEventData = new SetDateOfBirthEventData { DateOfBirth = command.DateOfBirth };
            var @event = _eventFactory.Create(command.ContextId, setNameEventData);
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

        public IEnumerable<IEvent> Execute(SetDateOfBirth2Command command)
        {
            var setNameEventData = new SetDateOfBirthEventDataV2 { DateOfBirth = command.DateOfBirth, HourOfBirth = 10 };
            var @event = _eventFactory.Create(command.ContextId, setNameEventData);
            return new[] { @event };
        }
    }
}