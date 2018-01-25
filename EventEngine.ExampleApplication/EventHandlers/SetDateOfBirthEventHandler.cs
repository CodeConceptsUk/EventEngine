using EventEngine.Attributes;
using EventEngine.ExampleApplication.Events;
using EventEngine.Interfaces.Events;

namespace EventEngine.ExampleApplication.EventHandlers
{
    [EventName("SetDateOfBirth")]
    [MaximumVersion(1)]
    public class SetDateOfBirthEventHandler : IEventEvaluator<ExampleView, SetDateOfBirthEventData>
    {
        public void Evaluate(ExampleView view, IEvent @event, SetDateOfBirthEventData eventData)
        {
            view.DateOfBirth = eventData.DateOfBirth;
        }
    }
}