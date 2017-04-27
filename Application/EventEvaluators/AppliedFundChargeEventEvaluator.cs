using Application.Events;
using Application.Interfaces;
using Application.Interfaces.Domain;
using Application.Views;

namespace Application.EventEvaluators
{
    public class AppliedFundChargeEventEvaluator : IEventEvaluator<AppliedFundChargeEvent, IPolicyContext, PolicyView>
    {
        public void Evaluate(PolicyView view, AppliedFundChargeEvent @event)
        {
            
        }
    }
}