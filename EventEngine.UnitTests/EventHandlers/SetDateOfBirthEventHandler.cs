using EventEngine.Application.Interfaces.Events;
using EventEngine.UnitTests.Events;

namespace EventEngine.UnitTests.EventHandlers
{
    public class SetDateOfBirthEventHandler : IEventEvaluator<SetDateOfBirthEvent, StateObject>
    {
        public void Evaluate(StateObject view, SetDateOfBirthEvent @event)
        {
            view.DateOfBirth = @event.DateOfBirth;
        }
    }
}