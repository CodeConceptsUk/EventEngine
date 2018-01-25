using EventEngine.Interfaces.Events;

namespace EventEngine.Interfaces.Services
{
    public interface IEventTypeService
    {
        IEventType Get<TEventData>()
            where TEventData : IEventData;
    }
}