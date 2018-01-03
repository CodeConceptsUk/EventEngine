using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Repositories;
using EventEngine.ExampleApplication.Interfaces.Queries;

namespace EventEngine.ExampleApplication.Queries
{
    public class ExampleViewQuery : EventQuery<ExampleView>, IExampleViewQuery
    {
        public ExampleViewQuery(IEventStore eventStore, IEventPlayer eventPlayer) 
            : base(eventStore, eventPlayer)
        {
        }
    }
}