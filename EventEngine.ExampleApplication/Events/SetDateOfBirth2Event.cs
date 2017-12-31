using System;
using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.UnitTests.Events
{
    [EventName("SetDateOfBirthEvent")]
    [Version(2)]
    public class SetDateOfBirth2Event : IEventData
    {
        public DateTime DateOfBirth { get; set; }

        public int HourOfBirth { get; set; }
    }
}