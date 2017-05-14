using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyTransactionsView;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.EventEvaluators.PolicyTransactionsView
{
    public class AddPremiumEventEvaluator : IEventEvaluator<AddPremiumEvent, PolicyTransactionView>
    {
        public void Evaluate(PolicyTransactionView view, AddPremiumEvent @event)
        {
            view.Transactions.Add(new Transaction
            {
                Type = "Add Premium Request",
                Value  = @event.PartitionDetails.Sum(p => p.Amount),
                TransactionDateTime = @event.PremiumDateTime
            });
        }
    }
}