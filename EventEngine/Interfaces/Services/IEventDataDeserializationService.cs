using System;

namespace EventEngine.Interfaces.Services
{
    public interface IEventDataDeserializationService
    {
        object Deserialize(Type eventDataType, string eventData);
    }
}