using System;
using EventEngine.Interfaces.Commands;

namespace EventEngine.ExampleApplication.Commands
{
    public class SetNameCommand : ICommand
    {
        public Guid ContextId { get; set; }

        public string Name { get; set; }
    }
}