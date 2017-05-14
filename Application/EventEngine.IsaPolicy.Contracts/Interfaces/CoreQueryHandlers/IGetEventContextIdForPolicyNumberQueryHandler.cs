using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.EventContextId;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers
{
    public interface IGetEventContextIdForPolicyNumberQueryHandler : IQueryHandler<GetEventContextIdForPolicyNumberQuery, EventContextIdView>
    {
    }
}