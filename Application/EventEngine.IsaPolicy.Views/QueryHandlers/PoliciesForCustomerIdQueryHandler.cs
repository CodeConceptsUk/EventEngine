using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PoliciesView;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.QueryHandlers
{
    public class PoliciesForCustomerIdQueryHandler : IQueryHandler<GetPoliciesForCustomerIdQuery, PoliciesView>
    {
        private readonly IGetEventContextIdsForCustomerIdQueryHandler _getEventContextIdForCustomerIdQuery;
        private readonly IQueryHandler<GetPolicyForContextIdQuery, PolicyView> _queryHandler;

        public PoliciesForCustomerIdQueryHandler(IGetEventContextIdsForCustomerIdQueryHandler getEventContextIdForCustomerIdQuery, IQueryHandler<GetPolicyForContextIdQuery, PolicyView> queryHandler)
        {
            _getEventContextIdForCustomerIdQuery = getEventContextIdForCustomerIdQuery;
            _queryHandler = queryHandler;
        }

        public PoliciesView Read(GetPoliciesForCustomerIdQuery getPoliciesForCustomerIdQuery)
        {
            var customerId = getPoliciesForCustomerIdQuery.CustomerId;
            var contextIds = _getEventContextIdForCustomerIdQuery.Read( new GetEventContextIdsForCustomerIdQuery(customerId));
            
            var policies = contextIds.EventContextIds.Select(ctx => _queryHandler.Read(new GetPolicyForContextIdQuery(ctx)));
            return new PoliciesView { Policies = policies };
        }
    }
}