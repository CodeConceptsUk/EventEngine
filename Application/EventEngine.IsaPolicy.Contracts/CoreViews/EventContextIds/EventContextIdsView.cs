using System;
using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.EventContextIds
{
    public class EventContextIdsView : IView
    {
        public EventContextIdsView(IEnumerable<Guid> eventContextIds)
        {
            EventContextIds = eventContextIds;
        }

        public IEnumerable<Guid> EventContextIds { get; set; }
    }
}