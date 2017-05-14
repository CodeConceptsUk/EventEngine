using System;
using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.EventContextIds
{
    public class EventContextIdsView : IsaPolicyView
    {
        public EventContextIdsView(IEnumerable<Guid> eventContextIds)
        {
            EventContextIds = eventContextIds;
        }

        public IEnumerable<Guid> EventContextIds { get; set; }
    }
}