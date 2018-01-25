using EventEngine.Attributes;
using EventEngine.ExampleApplication.Events;
using EventEngine.Interfaces.Events;

namespace EventEngine.ExampleApplication.EventHandlers
{
    [EventName("SetDateOfBirth")]
    [MinimumVersion(2)]
    public class SetDateOfBirthEventHandlerV2 : IEventEvaluator<ExampleView, SetDateOfBirthEventDataV2>
    {
        public void Evaluate(ExampleView view, IEvent @event, SetDateOfBirthEventDataV2 eventData)
        {
            view.DateOfBirth = eventData.DateOfBirth;
            view.HourOfBirth = eventData.HourOfBirth;
        }
    }
}