using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces.Events;
using EventEngine.ExampleApplication.Events;

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