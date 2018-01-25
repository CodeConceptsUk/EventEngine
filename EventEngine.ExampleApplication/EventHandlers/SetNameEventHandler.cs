using EventEngine.Attributes;
using EventEngine.ExampleApplication.Events;
using EventEngine.Interfaces.Events;

namespace EventEngine.ExampleApplication.EventHandlers
{
    [EventName("SetName")]
    public class SetNameEventHandler : IEventEvaluator<ExampleView, SetNameEventData>
    {
        public void Evaluate(ExampleView view, IEvent @event, SetNameEventData eventData)
        {
            view.Name = eventData.Name;
        }
    }
}