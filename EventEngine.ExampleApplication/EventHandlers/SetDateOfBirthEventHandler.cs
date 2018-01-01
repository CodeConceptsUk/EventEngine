using EventEngine.Application;
using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;
using EventEngine.ExampleApplication.Events;

namespace EventEngine.ExampleApplication.EventHandlers
{
    [EventName("SetDateOfBirthEvent")]
    [MaximumVersion(1)]
    public class SetDateOfBirthEventHandler : AbstractEventEvaluator<SetDateOfBirthEventData, ExampleView>
    {
        public SetDateOfBirthEventHandler(IEventDataDeserializationService eventDataDeserializationService) : base(eventDataDeserializationService)
        {
        }

        public override void Evaluate(ExampleView view, IEvent @event, SetDateOfBirthEventData eventData)
        {
            view.DateOfBirth = eventData.DateOfBirth;
        }
    }
}