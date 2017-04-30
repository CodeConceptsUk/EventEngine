using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Policy.Application.Interfaces.Repositories
{
    public interface IEventStoreRepository
    {
        IEnumerable<Guid> FindContextIds(Expression<Func<IEvent, bool>> where);

        IEnumerable<IEvent> Get(Guid eventContextId, Guid? afterEventId = null);

        void Add(IEnumerable<IEvent> events);
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
