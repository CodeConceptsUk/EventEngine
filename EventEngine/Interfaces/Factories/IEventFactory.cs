using System;
using EventEngine.Interfaces.Events;

namespace EventEngine.Interfaces.Factories
{
    public interface IEventFactory
    {
        IEvent Create<TEventData>(Guid contextId, TEventData eventData)
            where TEventData : IEventData;
    }
}