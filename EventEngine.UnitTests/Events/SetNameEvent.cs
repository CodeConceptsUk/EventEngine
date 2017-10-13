using EventEngine.Application.Interfaces.Events;

namespace EventEngine.UnitTests.Events
{
    public class SetNameEvent : IEvent
    {
        public string Name { get; set; }
    }
}