using System.Collections.Generic;
using EventEngine.Interfaces.Events;

namespace EventEngine.Interfaces.Services
{
    public interface IUndoEventProcessingService
    {
        IEnumerable<IEvent> Execute(IEnumerable<IEvent> events);
    }
}