using System.Collections.Generic;
using System.Linq;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.FrameworkExtensions.ObjectExtensions;
using log4net;

namespace CodeConcepts.EventEngine.Application
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IEnumerable<IQueryHandler> _handlers;
        private readonly ILog _logger;

        public QueryDispatcher(IEnumerable<IQueryHandler> handlers, ILogFactory logFactory)
        {
            _handlers = handlers;
            _logger = logFactory.GetLogger(typeof(QueryDispatcher));
        }

        public IView Read(IQuery query)
        {
            var handler = GetHandler(query);

            _logger.Debug($"Reading {query.GetType().Name} using {handler?.GetType()?.Name}");

            var results = handler?.Read(query.AsDynamic());
            return results;
        }

        private dynamic GetHandler(IQuery query)
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