using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;
using Newtonsoft.Json;

namespace EventEngine.Application.Services
{
    public class EventDataSerializationService : IEventDataSerializationService
    {
        public string Serialize(IEventData eventData)
        {
            return JsonConvert.SerializeObject(eventData, Formatting.Indented);
        }
    }
}