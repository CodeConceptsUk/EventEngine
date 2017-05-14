using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatus;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers
{
    public interface IPremiumsStatusQueryHandler : IQueryHandler<GetPremiumsStatusQuery, PremiumsStatusView>
    {
    }
}