using System;
using EventEngine.Interfaces.Services;
using Newtonsoft.Json;

namespace EventEngine.Services
{
    internal static class JsonHelper
    {
        public static JsonSerializer Serializer = JsonSerializer.Create(Settings);
        public static JsonSerializerSettings Settings = new JsonSerializerSettings {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            ObjectCreationHandling = ObjectCreationHandling.Auto,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            TypeNameHandling = TypeNameHandling.All,
            DateParseHandling = DateParseHandling.None,
            DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
            Formatting = Formatting.Indented
        };
    }

    public class EventDataDeserializationService : IEventDataDeserializationService
    {
        public object Deserialize(Type eventDataType, string eventData)
        {
            return JsonHelper.Serializer.DeserializeObject(eventData, eventDataType);
        }
    }
}
