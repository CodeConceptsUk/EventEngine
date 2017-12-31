using System;
using EventEngine.Application.Attributes;
using EventEngine.Application.Dispatchers;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.UnitTests.Events
{
    [EventName("SetDateOfBirthEvent")]
    [Version(1)]
    public class SetDateOfBirthEvent : IEventData
    {
        public DateTime DateOfBirth { get; set; }
    }
}