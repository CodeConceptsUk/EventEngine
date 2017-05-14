using System;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.EventContextId
{
    public class EventContextIdView : IView
    {
        public EventContextIdView(Guid eventContextId)
        {
            EventContextId = eventContextId;
        }

        public Guid EventContextId { get; set; }
    }
}