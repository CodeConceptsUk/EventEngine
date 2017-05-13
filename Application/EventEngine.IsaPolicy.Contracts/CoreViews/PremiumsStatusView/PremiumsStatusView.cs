using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatusView
{
    public class PremiumsStatusView : IView
    {
        public IList<string> PendingPremiumIds { get; set; } = new List<string>();

        public IList<string> ReceivedPremiumIds { get; set; } = new List<string>();

        public IList<string> AllocatedPremiumIds { get; set; } = new List<string>();
    }
}