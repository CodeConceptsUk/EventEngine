﻿using System;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.DataAccess.NoSql
{
    public class SinglePolicySnapshotNoSqlStore : ISnapshotStore<PolicyView>
    {
        public ISnapshot<PolicyView> Get(Guid contextId)
        {
            throw new NotImplementedException();
        }

        public void Add(PolicyView view, IEvent @event)
        {
            throw new NotImplementedException();
        }

        public void ClearAllSnapshots()
        {
            throw new NotImplementedException();
        }
    }
}