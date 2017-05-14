using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.EventContextId;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueryHandlers
{
    public class GetEventContextIdForPolicyNumberQueryHandler : IGetEventContextIdForPolicyNumberQueryHandler
    {
        private readonly IIsaPolicyEventStoreRepository _eventStore;

        public GetEventContextIdForPolicyNumberQueryHandler(IIsaPolicyEventStoreRepository eventStore)
        {
            _eventStore = eventStore;
        }
        
        public EventContextIdView Read(GetEventContextIdForPolicyNumberQuery query)
        {
            var eventContextId = _eventStore.FindContextId(query.PolicyNumber);
            return !eventContextId.HasValue
                ? null
                : new EventContextIdView(eventContextId.Value);
        }
    }
}