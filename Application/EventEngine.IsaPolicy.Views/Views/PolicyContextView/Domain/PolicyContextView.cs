using System;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyContextView.Domain
{
    public class PolicyContextView : IView
    {
        public Guid EventContextId { get; set; }
    }
}