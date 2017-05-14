using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyTransactionsView;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.EventEvaluators.PolicyTransactionsView
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