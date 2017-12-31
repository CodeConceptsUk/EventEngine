using EventEngine.Application.Interfaces.Events;

namespace EventEngine.UnitTests.Events
{
    public class SetNameEvent : IEventData
    {
        public string Name { get; set; }
    }
}