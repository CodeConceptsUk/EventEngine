using EventEngine.Application.Interfaces.Events;

namespace EventEngine.UnitTests.Events
{
    public class NameEvent : IEvent
    {
        public string Name { get; set; }
    }
}