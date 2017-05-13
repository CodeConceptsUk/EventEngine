using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.Application.Interfaces.Factories
{
    public interface IQueryDispatcherFactory
    {
        IQueryDispatcher<TQuery> Create<TQuery>()
            where TQuery : class, IQuery;
    }
}