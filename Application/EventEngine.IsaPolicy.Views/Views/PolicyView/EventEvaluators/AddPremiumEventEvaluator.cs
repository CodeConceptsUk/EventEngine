using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyView.Domain;
using Microsoft.Practices.ObjectBuilder2;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyView.EventEvaluators
{
    public class AddPremiumEventEvaluator : IEventEvaluator<AddPremiumEvent, Domain.PolicyView>
    {
        public void Evaluate(Domain.PolicyView view, AddPremiumEvent @event)
        {
            var premium = new Premium
            {
                PremiumId = @event.PremiumId,
                Total = @event.PartitionDetails.Sum(p => p.Amount),
                IsAllocated = false
            };
            view.Premiums.Add(premium);

            @event.PartitionDetails.ForEach(p =>
            {
                var partition = new PremiumPartition
                {
                    FundId = p.FundId,
                    Amount = p.Amount,
                    PortionId = p.PartitionId
                };
                premium.Partitions.Add(partition);
            });
        }
    }
}