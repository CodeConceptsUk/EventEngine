using System;

namespace CodeConcepts.EventEngine.Contracts.Interfaces.Repositories
{
    public interface ISnapshotStore
    {
        
    }

    public interface ISnapshotStore<TView> : ISnapshotStore
        where TView : class, IView
    {
        ISnapshot<TView> Get(Guid contextId);

        void Add(TView view, IEvent @event);

        void ClearAllSnapshots();
    }
}