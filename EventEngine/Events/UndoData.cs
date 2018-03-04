using System;
using System.Collections.Generic;
using EventEngine.Attributes;
using EventEngine.Interfaces.Events;

namespace EventEngine.Events
{
    [EventName("Undo")]
    public class UndoData : IEventData
    {
        public UndoData(IEnumerable<Guid> eventIds)
        {
            EventIds = eventIds;
        }

        public IEnumerable<Guid> EventIds { get; set; }
    }
}