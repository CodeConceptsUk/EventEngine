using System;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;

namespace Policy.Plugin.Isa.Policy.Views
{
    public class PolicyFundsView : IView<IPolicyContext>
    {
        public DateTime LastCalculatedEventAt { get; set; }
        public Guid LastCalculatedEventId { get; set; }

    }
}