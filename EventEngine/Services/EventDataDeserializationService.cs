using System;
using EventEngine.Interfaces.Services;
using Newtonsoft.Json;

namespace EventEngine.Services
{
    public class EventDataDeserializationService : IEventDataDeserializationService
    {
        public object Deserialize(Type eventDataType, string eventData)
        {
            return JsonConvert.DeserializeObject(eventData, eventDataType, JsonSettingsHelper.Settings);
        }
    }
}
