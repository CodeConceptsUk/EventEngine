using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.Domain;

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.EventEvaluators
{
    public class UnitsAddedToFundEventEvaluator : IEventEvaluator<UnitsAddedToFundEvent, PolicyTransactionView>
    {
        public void Evaluate(PolicyTransactionView view, UnitsAddedToFundEvent @event)
        {
            view.Transactions.Add(new Transaction
            {
                Type = $"Adhoc units added {@event.FundId}",
                Value = @event.Units,
                TransactionDateTime = @event.DateTimeAdded
            });
        }
    }
}