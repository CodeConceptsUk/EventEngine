using System.Collections.Generic;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.ExampleApplication.Commands;
using EventEngine.ExampleApplication.Events;

namespace EventEngine.ExampleApplication.CommandHandlers
{
    public class SetNameCommandHandler : ICommandHandler<SetNameCommand>
    {
        private readonly IEventFactory _eventFactory;

        public SetNameCommandHandler(IEventFactory eventFactory)
        {
            _eventFactory = eventFactory;
        }

        public IEnumerable<IEvent> Execute(SetNameCommand command)
        {
            var setNameEventData = new SetNameEventData { Name = command.Name };
            var @event = _eventFactory.Create(command.ContextId, setNameEventData);
            return new[] { @event };
        }
    }
}