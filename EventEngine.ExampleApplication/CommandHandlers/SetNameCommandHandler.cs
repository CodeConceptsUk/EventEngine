using System.Collections.Generic;
using EventEngine.ExampleApplication.Commands;
using EventEngine.ExampleApplication.Events;
using EventEngine.Interfaces.Commands;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Factories;

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