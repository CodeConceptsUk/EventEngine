using System;

namespace Policy.Application.Interfaces.Repositories
{
    public interface ISnapshotStore<TView, TContext>
        where TContext : class, IContext
        where TView : class, IView<TContext>
    {
        ISnapshot<TView, TContext> Get(Guid contextId);

        void Add(TView view, IEvent<TContext> @event);
    }
}