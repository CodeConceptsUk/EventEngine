using EventEngine.Application;
using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;
using EventEngine.ExampleApplication.Events;

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