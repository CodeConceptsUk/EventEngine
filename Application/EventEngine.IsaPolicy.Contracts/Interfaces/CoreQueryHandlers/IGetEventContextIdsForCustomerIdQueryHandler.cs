using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.EventContextIds;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers
{
    public interface IGetEventContextIdsForCustomerIdQueryHandler : IQueryHandler<GetEventContextIdsForCustomerIdQuery, EventContextIdsView>
    {
    }
}