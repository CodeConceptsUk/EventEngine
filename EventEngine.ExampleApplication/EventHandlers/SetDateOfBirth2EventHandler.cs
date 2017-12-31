using EventEngine.Application;
using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces.Events;
using EventEngine.UnitTests.Events;

namespace EventEngine.UnitTests.EventHandlers
{
    [EventName("SetDateOfBirthEvent")]
    [MinimumVersion( 2)]
    public class SetDateOfBirth2EventHandler : AbstractEventEvaluator<SetDateOfBirth2Event, StateObject>
    {
        public override void Evaluate(StateObject view, IEvent @event, SetDateOfBirth2Event eventData)
        {
            view.DateOfBirth = eventData.DateOfBirth.AddDays(1);
        }
    }
}