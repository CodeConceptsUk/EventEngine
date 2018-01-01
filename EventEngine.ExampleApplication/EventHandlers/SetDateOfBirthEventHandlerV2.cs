using EventEngine.Application;
using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;
using EventEngine.ExampleApplication.Events;

namespace EventEngine.ExampleApplication.EventHandlers
{
    [EventName("SetDateOfBirth")]
    [MinimumVersion( 2)]
    public class SetDateOfBirthEventHandlerV2 : AbstractEventEvaluator<SetDateOfBirthEventDataV2, ExampleView>
    {
        public SetDateOfBirthEventHandlerV2(IEventDataDeserializationService eventDataDeserializationService) : base(eventDataDeserializationService)
        {
        }

        public override void Evaluate(ExampleView view, IEvent @event, SetDateOfBirthEventDataV2 eventData)
        {
            view.DateOfBirth = eventData.DateOfBirth;
            view.HourOfBirth = eventData.HourOfBirth;
        }
    }
}