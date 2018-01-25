using EventEngine.Attributes;
using EventEngine.Interfaces.Events;

namespace EventEngine.ExampleApplication.Events
{
    [EventName("SetName")]
    public class SetNameEventData : IEventData
    {
        public string Name { get; set; }
    }
}