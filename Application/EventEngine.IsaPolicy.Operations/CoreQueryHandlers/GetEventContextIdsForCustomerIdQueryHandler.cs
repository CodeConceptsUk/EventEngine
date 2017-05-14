using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.EventContextIds;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueryHandlers
{
    public class GetEventContextIdsForCustomerIdQueryHandler : IGetEventContextIdsForCustomerIdQueryHandler
    {
        private readonly IIsaPolicyEventStoreRepository _eventStore;

        public GetEventContextIdsForCustomerIdQueryHandler(IIsaPolicyEventStoreRepository eventStore)
        {
            _eventStore = eventStore;
        }
        

        public EventContextIdsView Read(GetEventContextIdsForCustomerIdQuery query)
        {
            var contextIds = _eventStore.FindContextIds(query.CustomerId);
            return new EventContextIdsView(contextIds);
        }
    }
}