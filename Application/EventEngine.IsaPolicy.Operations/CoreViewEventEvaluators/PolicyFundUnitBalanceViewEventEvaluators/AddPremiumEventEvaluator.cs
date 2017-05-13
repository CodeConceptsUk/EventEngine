using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalanceView;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.FrameworkExtensions.LinqExtensions;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreViewEventEvaluators.PolicyFundUnitBalanceViewEventEvaluators
{
    public class AddPremiumEventEvaluator : IEventEvaluator<AddPremiumEvent, PolicyFundUnitBalanceView>
    {
        public void Evaluate(PolicyFundUnitBalanceView view, AddPremiumEvent @event)
        {
            //var premium = new Premium
            //{
            //    PremiumId = @event.PremiumId,
            //    Total = @event.PartitionDetails.Sum(p => p.Amount),
            //    IsAllocated = false
            //};
            //view.Premiums.Add(premium);

            //@event.PartitionDetails.ForEach(p =>
            //{
            //    var partition = new PremiumPartition
            //    {
            //        FundId = p.FundId,
            //        Amount = p.Amount,
            //        PortionId = p.PartitionId
            //    };
            //    premium.Partitions.Add(partition);
            //});
        }
    }
}