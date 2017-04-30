using System;
using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Events
{
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