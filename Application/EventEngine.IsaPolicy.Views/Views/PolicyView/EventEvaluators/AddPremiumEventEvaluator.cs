using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyView.Domain;

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyView.EventEvaluators
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