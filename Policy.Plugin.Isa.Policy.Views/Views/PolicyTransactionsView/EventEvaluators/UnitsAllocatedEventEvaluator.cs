using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.Domain;

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.EventEvaluators
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