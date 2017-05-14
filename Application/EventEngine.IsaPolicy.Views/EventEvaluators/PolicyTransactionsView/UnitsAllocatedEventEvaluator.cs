using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyTransactionsView;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.EventEvaluators.PolicyTransactionsView
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