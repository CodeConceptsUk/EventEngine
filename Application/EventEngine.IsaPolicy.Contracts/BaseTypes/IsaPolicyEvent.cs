using System;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.PluginSupport;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes
{
    [PluginBaseType]
    public abstract class IsaPolicyEvent : IEvent
    {
        protected IsaPolicyEvent(Guid eventContextId)
        {
            EventContextId = eventContextId;
        }

        public Guid EventContextId { get; set; }

        public Guid EventId { get; set; } = Guid.NewGuid();

        public DateTime EventDateTime { get; set; } = DateTime.Now;
    }
}