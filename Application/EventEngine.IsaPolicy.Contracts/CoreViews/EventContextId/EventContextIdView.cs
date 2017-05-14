using System;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.EventContextId
{
    public class EventContextIdView : IsaPolicyView
    {
        public EventContextIdView(Guid eventContextId)
        {
            EventContextId = eventContextId;
        }

        public Guid EventContextId { get; set; }
    }
}