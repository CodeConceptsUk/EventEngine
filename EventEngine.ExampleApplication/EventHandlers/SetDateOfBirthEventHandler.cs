using System;
using System.Linq;
using EventEngine.Application.Attributes;
using EventEngine.Application;
using EventEngine.Application.Interfaces.Events;
using EventEngine.UnitTests.Events;

namespace EventEngine.UnitTests.EventHandlers
{
    [EventName("SetDateOfBirthEvent")]
    [MinimumVersion(1)]
    public class SetDateOfBirthEventHandler : AbstractEventEvaluator<SetDateOfBirthEvent, StateObject>
    {
        public override void Evaluate(StateObject view, IEvent @event, SetDateOfBirthEvent eventData)
        {
            view.DateOfBirth = eventData.DateOfBirth;
        }
    }
}