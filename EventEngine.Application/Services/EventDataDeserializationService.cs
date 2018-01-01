using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;
using Newtonsoft.Json;

namespace EventEngine.Application.Services
{
    public class EventDataDeserializationService : IEventDataDeserializationService
    {
        public TEventData Deserialize<TEventData>(string eventData)
            where TEventData : IEventData
        {
            return JsonConvert.DeserializeObject<TEventData>(eventData);
        }
    }
}