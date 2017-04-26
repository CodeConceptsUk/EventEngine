using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Application.Interfaces;
using Application.Interfaces.Domain;
using Application.Interfaces.Repositories;
using Microsoft.Practices.ObjectBuilder2;

namespace DataAccess
{
    public class PolicyContextEventStoreRepository : IEventStoreRepository<IPolicyContext>
    {
        private static readonly IList<IEvent<IPolicyContext>> Events = new List<IEvent<IPolicyContext>>();
        
        public IEnumerable<Guid> FindContextIds(Expression<Func<IEvent<IPolicyContext>, bool>> @where)
        {
            var expression = @where.Compile();
            var events = Events.Where(t => expression(t)).Select(t => t.EventContextId);
            return events;
        }

        public IEnumerable<IEvent<IPolicyContext>> Get(Guid eventContextId)
        {
            return Events.Where(t => t.EventContextId == eventContextId);
        }

        public void Add(IEnumerable<IEvent<IPolicyContext>> events)
        {
            events.ForEach(t => Events.Add(t));
        }
    }
}