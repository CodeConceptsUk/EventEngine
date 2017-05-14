using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiums;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers
{
    public interface IUnallocatedReceivedPremiumsQueryHandler : IQueryHandler<GetUnallocatedReceivedPremiumsQuery, UnallocatedReceivedPremiumsView>
    {
    }
}