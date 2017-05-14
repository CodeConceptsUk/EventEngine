using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.EventContextIds;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueryHandlers
{
    public class GetEventContextIdsForCustomerIdQueryHandler : IQueryHandler<GetEventContextIdsForCustomerIdQuery, EventContextIdsView>
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