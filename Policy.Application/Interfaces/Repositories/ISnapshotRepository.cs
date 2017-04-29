using System;

namespace Policy.Application.Interfaces.Repositories
{
    public interface ISnapshotRepository<TContext, TView>
        where TContext : class, IContext
        where TView : IView<TContext>
    {
        TView GetViewAtOrBefore(Guid eventContextId, DateTime expectedDateTime);

        void SaveView(TView view, Guid eventContextId, DateTime viewValidDateTime);
    }
}