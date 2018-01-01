using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application.Interfaces.Services
{
    public interface IEventDataDeserializationService
    {
        TEventData Deserialize<TEventData>(string eventData)
            where TEventData : IEventData;
    }
}