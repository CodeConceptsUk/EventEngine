using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using SimpleInjector;

namespace CodeConcepts.EventEngine.Application.Factories
{
    public class QueryDispatcherFactory : IQueryDispatcherFactory
    {
        private readonly Container _container;
        private readonly ILogFactory _logFactory;

        public QueryDispatcherFactory(Container container, ILogFactory logFactory)
        {
            _container = container;
            _logFactory = logFactory;
        }

        public IQueryDispatcher<TQuery> Create<TQuery>() 
            where TQuery : class, IQuery
        {
            var handlers = _container.GetAllInstances<IQueryHandler>();
            return new QueryDispatcher<TQuery>(handlers, _logFactory);
        }
    }
}