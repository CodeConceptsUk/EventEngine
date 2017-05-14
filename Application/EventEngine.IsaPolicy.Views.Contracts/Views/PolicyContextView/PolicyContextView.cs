using System;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyContextView
{
    public class PolicyContextView : IView
    {
        public Guid EventContextId { get; set; }
    }
}