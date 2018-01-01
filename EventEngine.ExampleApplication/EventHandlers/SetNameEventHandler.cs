using EventEngine.Application;
using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;
using EventEngine.ExampleApplication.Events;

namespace EventEngine.ExampleApplication.EventHandlers
{
    [EventName("SetName")]
    public class SetNameEventHandler : AbstractEventEvaluator<SetNameEventData, ExampleView>
    {
        public SetNameEventHandler(IEventDataDeserializationService eventDataDeserializationService) : base(eventDataDeserializationService)
        {
        }

        public override void Evaluate(ExampleView view, IEvent @event, SetNameEventData eventData)
        {
            view.Name = eventData.Name;
        }
    }
}