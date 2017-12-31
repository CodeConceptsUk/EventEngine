using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application.Interfaces.Services
{
    public interface IEventDataSerializationService
    {
        string Serialize(IEventData eventData);
    }

    public interface IEventDataDeserializationService
    {
        TEventData Serialize<TEventData>(string eventData)
            where TEventData : IEventData;
    }
}