using System.Collections.Generic;
using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries
{
    public class PoliciesForCustomerIdQueryHandler : IQueryHandler<GetPoliciesForCustomerIdQuery, PoliciesView>
    {
        private readonly IGetEventContextIdForCustomerIdQuery _getEventContextIdForCustomerIdQuery;
        private readonly IQueryHandler<GetPolicyForContextIdQuery, PolicyView> _queryHandler;

        public PoliciesForCustomerIdQueryHandler(IGetEventContextIdForCustomerIdQuery getEventContextIdForCustomerIdQuery, IQueryHandler<GetPolicyForContextIdQuery, PolicyView> queryHandler)
        {
            _getEventContextIdForCustomerIdQuery = getEventContextIdForCustomerIdQuery;
            _queryHandler = queryHandler;
        }

        public PoliciesView Read(GetPoliciesForCustomerIdQuery forPolicyNumberQuery)
        {
            var customerId = forPolicyNumberQuery.CustomerId;
            var contextIds = _getEventContextIdForCustomerIdQuery.GetEventContextIds(customerId);
            
            var policies = contextIds.Select(ctx => _queryHandler.Read(new GetPolicyForContextIdQuery(ctx)));
            return new PoliciesView { Policies = policies };
        }
    }
}