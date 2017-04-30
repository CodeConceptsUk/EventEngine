using System;

namespace Policy.Application.Interfaces.Repositories
{
    public interface ISnapshotStore<TView>
        where TView : class, IView
    {
        ISnapshot<TView> Get(Guid contextId);

        void Add(TView view, IEvent @event);
    }
}