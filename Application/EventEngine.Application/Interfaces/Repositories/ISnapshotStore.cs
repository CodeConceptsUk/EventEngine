using System;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.Application.Interfaces.Repositories
{
    public interface ISnapshotStore<TView>
        where TView : class, IView
    {
        ISnapshot<TView> Get(Guid contextId);

        void Add(TView view, IEvent @event);

        void ClearAllSnapshots();
    }
}