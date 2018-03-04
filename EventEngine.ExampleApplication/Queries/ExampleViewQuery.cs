using EventEngine.ExampleApplication.Interfaces.Queries;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Repositories;
using EventEngine.Interfaces.Services;
using EventEngine.Queries;

namespace EventEngine.ExampleApplication.Queries
{
    public class ExampleViewQuery : EventQuery<ExampleView>, IExampleViewQuery
    {
        public ExampleViewQuery(IEventStore eventStore, IEventPlayer eventPlayer, IEventDataDeserializationService eventDataDeserializationService) 
            : base(eventStore, eventPlayer, eventDataDeserializationService)
        {
        }
    }
}