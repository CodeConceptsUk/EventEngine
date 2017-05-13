using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Events.Events;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.EventEvaluators
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