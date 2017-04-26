using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IEventStoreRepository<TContext>
        where TContext : class
    {
        IEnumerable<Guid> FindContextIds(Expression<Func<IEvent<TContext>, bool>> where);

        IEnumerable<IEvent<TContext>> Get(Guid eventContextId);

        void Add(IEnumerable<IEvent<TContext>> events);
    }

    //public interface 
}

//Policy 1, 1234
//Customer 1
//  Add Fund ..
//  Add Charge ..
// Meta data 1: Policy 1234, 1

//Policy 2, 5678
//Customer 2
//  Add Fund ..
//  Add Charge ..
// Meta data 2: Policy 5678, 2
