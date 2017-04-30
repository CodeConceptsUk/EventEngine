using System;
using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Views.PolicyContextView.Domain
{
    public class PolicyContextView : IView
    {
        public Guid EventContextId { get; set; }
    }
}