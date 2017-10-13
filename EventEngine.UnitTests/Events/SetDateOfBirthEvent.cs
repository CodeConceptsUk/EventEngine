using System;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.UnitTests.Events
{
    public class SetDateOfBirthEvent : IEvent
    {
        public DateTime DateOfBirth { get; set; }
    }
}