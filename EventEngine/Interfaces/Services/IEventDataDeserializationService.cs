using System;

namespace EventEngine.Application.Interfaces.Services
{
    public interface IEventDataDeserializationService
    {
        object Deserialize(Type eventDataType, string eventData);
    }
}