using System.Linq;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyView.EventEvaluators
{
    public class PremiumReceivedEventEvaluator : IEventEvaluator<PremiumReceivedEvent, Domain.PolicyView>
    {
        public void Evaluate(Domain.PolicyView view, PremiumReceivedEvent @event)
        {
            view.Premiums.Single(p => p.PremiumId == @event.PremiumId).IsReceived = true;
        }
    }
}