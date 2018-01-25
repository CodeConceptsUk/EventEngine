using System;
using EventEngine.Interfaces.Commands;

namespace EventEngine.ExampleApplication.Commands
{
    public class SetDateOfBirthCommand : ICommand
    {
        public Guid ContextId { get; set; }

        public DateTime DateOfBirth { get; set; }
    }

    public class SetDateOfBirth2Command : ICommand
    {
        public Guid ContextId { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}