using System;
using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.ExampleApplication.Events
{
    [EventName("SetDateOfBirth")]
    public class SetDateOfBirthEventData : IEventData
    {
        public DateTime DateOfBirth { get; set; }
    }
}