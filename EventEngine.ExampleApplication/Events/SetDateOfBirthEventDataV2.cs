using System;
using EventEngine.Attributes;
using EventEngine.Interfaces.Events;

namespace EventEngine.ExampleApplication.Events
{
    [EventName("SetDateOfBirth")]
    [Version(2)]
    public class SetDateOfBirthEventDataV2 : IEventData
    {
        public DateTime DateOfBirth { get; set; }

        public int HourOfBirth { get; set; }
    }
}