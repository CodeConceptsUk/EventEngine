using System.Collections.Generic;
using System.Linq;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.FrameworkExtensions.LinqExtensions;
using CodeConcepts.FrameworkExtensions.ObjectExtensions;
using log4net;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.Application
{
    public class QueryDispatcher<TQuery> : IQueryDispatcher<TQuery>
        where TQuery : class, IQuery
    {
        private readonly IList<IQueryHandler> _handlers = new List<IQueryHandler>();
        private readonly ILog _logger;

        public QueryDispatcher(IUnityContainer container, ILogFactory logFactory)
        {
            _logger = logFactory.GetLogger(typeof(QueryDispatcher<>));
            var handlers = container.ResolveAll(typeof(IQueryHandler));
            handlers.ForEach(handler => _handlers.Add((IQueryHandler)handler));
        }

        public IView Read(TQuery query)
        {
            var handler = GetHandler(query);

            _logger.Debug($"Reading {query.GetType().Name} using {handler?.GetType()?.Name}");

            var results = handler?.Read(query.AsDynamic());
            return results;
        }

        private dynamic GetHandler(TQuery query)
        {
            {
                return _handlers.Where(t => t.GetType()
                        .GetInterfaces()
                        .Any(i => i.GetGenericArguments().Contains(query.GetType())))
                    .Select(t => t.AsDynamic()).Single();
            }
        }
    }
}