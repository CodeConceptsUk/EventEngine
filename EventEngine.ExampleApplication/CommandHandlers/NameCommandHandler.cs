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
        public static IEvent Event = Substitute.For<IEvent>();

        public IEnumerable<IEvent> Execute(NameCommand command)
        {
            return new[] { Event };
        }
    }
}