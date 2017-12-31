using EventEngine.Application;
using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces.Events;
using EventEngine.UnitTests.Events;

namespace EventEngine.UnitTests.EventHandlers
{
    [EventName("SetNameEvent")]
    public class SetNameEventHandler : AbstractEventEvaluator<SetNameEvent, StateObject>
    {
        public override void Evaluate(StateObject view, IEvent @event, SetNameEvent eventData)
        {
            view.Name = eventData.Name;
        }
    }

    
}