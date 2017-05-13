using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Events.Events;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.EventEvaluators
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