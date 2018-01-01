using System;
using EventEngine.Application.Interfaces.Commands;

namespace EventEngine.ExampleApplication.Commands
{
    public class SetDateOfBirthCommand : ICommand
    {
        public Guid ContextId { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}