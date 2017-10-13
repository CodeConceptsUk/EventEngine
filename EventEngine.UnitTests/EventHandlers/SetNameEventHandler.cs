using EventEngine.Application.Interfaces.Events;
using EventEngine.UnitTests.Events;

namespace EventEngine.UnitTests.EventHandlers
{
    public class SetNameEventHandler : IEventEvaluator<SetNameEvent, StateObject>
    {
        public void Evaluate(StateObject view, SetNameEvent @event)
        {
            view.Name = @event.Name;
        }
    }
}