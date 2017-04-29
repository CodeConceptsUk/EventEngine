using System;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;

namespace Policy.Plugin.Isa.Policy.Views
{
    public class PolicyContextView : IView<IPolicyContext>
    {
        public Guid EventContextId { get; set; }
        public DateTime LastCalculatedEventAt { get; set; }
        public Guid LastCalculatedEventId { get; set; }
    }
}