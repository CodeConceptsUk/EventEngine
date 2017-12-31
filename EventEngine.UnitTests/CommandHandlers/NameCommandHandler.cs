using System;
using System.Collections.Generic;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Repositories;
using EventEngine.Application.PropertyBags;
using EventEngine.UnitTests.Commands;
using EventEngine.UnitTests.Events;
using NSubstitute;
namespace EventEngine.UnitTests.CommandHandlers
{
    public class NameCommandHandler : ICommandHandler<NameCommand>
    {
        public IEnumerable<IEvent> Execute(NameCommand command)
        {
            var eventType = Substitute.For<IEventType>();
            var eventData = new SetNameEvent { Name = command.Name };
            eventType.Type.Returns(GetType().FullName);

            return new[] { new Event<SetNameEvent>(Guid.NewGuid(), eventType, eventData, DateTime.Now) };
        }
    }
}