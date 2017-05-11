using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.Domain;

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.EventEvaluators
{
    public class AppliedFundChargeEventEvaluator : IEventEvaluator<AppliedFundChargeEvent, PolicyTransactionView>
    {
        public void Evaluate(PolicyTransactionView view, AppliedFundChargeEvent @event)
        {
            view.Transactions.Add(new Transaction
            {
                Type = $"Fund charge applied {@event.FundId}",
                Value = @event.Units,
                TransactionDateTime = @event.ChargeDate
            });
        }
    }
}