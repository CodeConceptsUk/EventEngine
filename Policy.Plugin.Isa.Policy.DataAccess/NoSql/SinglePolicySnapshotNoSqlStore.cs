using System;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyView.Domain;

namespace Policy.Plugin.Isa.Policy.DataAccess.NoSql
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
    }
}