using CodeConcepts.EventEngine.Application.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Events.Events;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.EventEvaluators
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