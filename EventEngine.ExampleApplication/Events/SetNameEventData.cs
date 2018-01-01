using EventEngine.Application.Attributes;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.ExampleApplication.Events
{
    [EventName("SetName")]
    public class SetNameEventData : IEventData
    {
        public string Name { get; set; }
    }
}