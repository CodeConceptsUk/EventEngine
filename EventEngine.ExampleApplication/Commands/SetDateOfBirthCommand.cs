using System;
using EventEngine.Interfaces.Commands;

namespace EventEngine.ExampleApplication.Commands
{
    public class SetDateOfBirthCommand : ICommand
    {
        public DateTime DateOfBirth { get; set; }
    }

    public class SetDateOfBirth2Command : ICommand
    {
        public DateTime DateOfBirth { get; set; }
    }
}