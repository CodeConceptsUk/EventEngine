using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application.Interfaces.Services
{
    public interface IEventTypeService
    {
        IEventType Get<TEventData>()
            where TEventData : IEventData;
    }
}