using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyTransactionsView;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.EventEvaluators.PolicyTransactionsView
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