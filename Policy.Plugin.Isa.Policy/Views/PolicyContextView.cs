using System;
using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Views
{
    public class PolicyContextView : IView
    {
        public Guid EventContextId { get; set; }
    }
}