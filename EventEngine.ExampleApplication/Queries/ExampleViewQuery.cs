using EventEngine.ExampleApplication.Interfaces.Queries;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Repositories;

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