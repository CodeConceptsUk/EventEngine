using System.Collections.Generic;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatus
{
    public class PremiumsStatusView : IsaPolicyView
    {
        public IList<string> PendingPremiumIds { get; set; } = new List<string>();

        public IList<string> ReceivedPremiumIds { get; set; } = new List<string>();

        public IList<string> AllocatedPremiumIds { get; set; } = new List<string>();
    }
}