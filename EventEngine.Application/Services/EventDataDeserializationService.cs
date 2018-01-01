using System;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;
using Newtonsoft.Json;

namespace EventEngine.Application.Services
{
    public class EventDataDeserializationService : IEventDataDeserializationService
    {
        public object Deserialize(Type eventDataType, string eventData)
        {
            return JsonConvert.DeserializeObject(eventData, eventDataType);
        }
    }
}