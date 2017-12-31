using EventEngine.Application.Dispatchers;
using EventEngine.Application.Interfaces.Events;
using EventEngine.UnitTests.Events;

namespace EventEngine.UnitTests.EventHandlers
{
    [MinimumVersion(1)]
    public class SetNameEventHandler : IEventEvaluator<SetNameEvent, StateObject>
    {
        public void Evaluate(StateObject view, IEvent<SetNameEvent> @event)
        {
            view.Name = @event.EventData.Name;
        }
    }

    
}