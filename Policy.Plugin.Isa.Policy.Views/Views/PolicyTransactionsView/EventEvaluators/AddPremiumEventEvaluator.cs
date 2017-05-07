using System.Linq;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.Domain;

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.EventEvaluators
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