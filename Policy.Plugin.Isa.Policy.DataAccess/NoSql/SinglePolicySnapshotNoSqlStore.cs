using System;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Views.PolicyView;

namespace Policy.Plugin.Isa.Policy.DataAccess.NoSql
{
    public class SinglePolicySnapshotNoSqlStore : ISnapshotStore<PolicyView, IPolicyContext>
    {
        public ISnapshot<PolicyView, IPolicyContext> Get(Guid contextId)
        {
            throw new NotImplementedException();
        }

        public void Add(PolicyView view, IEvent<IPolicyContext> @event)
        {
            throw new NotImplementedException();
        }
    }
}