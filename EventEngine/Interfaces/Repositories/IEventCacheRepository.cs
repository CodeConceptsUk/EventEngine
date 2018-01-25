using System;

namespace EventEngine.Interfaces.Repositories
{
    public interface IEventCacheRepository
    {
        void Set<TState, TIdentifier>(TState state, TIdentifier key, DateTime pointInTime);

        (TState State, DateTime PointInTime) Get<TState, TIdentifier>(TIdentifier key, DateTime pointInTime);
    }
}