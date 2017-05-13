using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreViewEventEvaluators.PolicyFundBalanceView
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