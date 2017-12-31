using System;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application.Interfaces.Factories
{
    public interface IEventFactory
    {
        IEvent Create(Guid contextId, IEventData eventData);
    }
}