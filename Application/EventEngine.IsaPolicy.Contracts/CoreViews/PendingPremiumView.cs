using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces
{
    public class PendingPremiumView : IView
    {
        public string PremiumId { get; set; }
    }
}