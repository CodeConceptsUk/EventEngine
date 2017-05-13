using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Events.Events;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.EventEvaluators
{
    public class UnitsAllocatedEventEvaluator : IEventEvaluator<UnitsAllocatedEvent, PolicyTransactionView>
    {
        public void Evaluate(PolicyTransactionView view, UnitsAllocatedEvent @event)
        {
            view.Transactions.Add(new Transaction
            {
                Type = $"Units allocated {@event.FundId}",
                Value = @event.Units,
                TransactionDateTime = @event.AllocationDateTime
            });
        }
    }
}