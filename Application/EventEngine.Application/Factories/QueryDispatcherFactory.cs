using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.Application.Factories
{
    public class QueryDispatcherFactory : IQueryDispatcherFactory
    {
        private readonly IUnityContainer _unityContainer;
        private readonly ILogFactory _logFactory;

        public QueryDispatcherFactory(IUnityContainer unityContainer, ILogFactory logFactory)
        {
            _unityContainer = unityContainer;
            _logFactory = logFactory;
        }

        public IQueryDispatcher<TQuery> Create<TQuery>() 
            where TQuery : class, IQuery
        {
            return new QueryDispatcher<TQuery>(_unityContainer, _logFactory);
        }
    }
}