using System;
using EventEngine.Attributes;
using EventEngine.Interfaces.Events;

namespace EventEngine.ExampleApplication.Events
{
    [EventName("SetDateOfBirth")]
    public class SetDateOfBirthEventData : IEventData
    {
        public DateTime DateOfBirth { get; set; }
    }
}