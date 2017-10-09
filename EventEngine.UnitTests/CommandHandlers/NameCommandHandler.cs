using System.Collections.Generic;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.UnitTests.Commands;
using EventEngine.UnitTests.Events;

namespace EventEngine.UnitTests.CommandHandlers
{
    public class NameCommandHandler : ICommandHandler<NameCommand>
    {
        public IEnumerable<IEvent> Execute(NameCommand command)
        {
            return new[] { new NameEvent { Name = command.Name } };
        }
    }
}