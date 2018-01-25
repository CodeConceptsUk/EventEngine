using EventEngine.Interfaces.Events;

namespace EventEngine.Interfaces.Services
{
    public interface IEventDataSerializationService
    {
        string Serialize(IEventData eventData);
    }
}