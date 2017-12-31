using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.UnitTests.Events
{
    [EventName("SetNameEvent")]
    public class SetNameEvent : IEventData
    {
        public string Name { get; set; }
    }
}