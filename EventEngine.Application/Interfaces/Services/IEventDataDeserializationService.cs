using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application.Interfaces.Services
{
    public interface IEventDataDeserializationService
    {
        TEventData Serialize<TEventData>(string eventData)
            where TEventData : IEventData;
    }
}