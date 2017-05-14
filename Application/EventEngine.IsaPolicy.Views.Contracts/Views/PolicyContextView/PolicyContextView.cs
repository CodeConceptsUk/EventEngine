using System;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyContextView
{
    public class PolicyContextView : IsaPolicyView
    {
        public Guid EventContextId { get; set; }
    }
}