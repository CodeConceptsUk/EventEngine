using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries
{
    public class PolicyQueryHandler : IQueryHandler<GetSinglePolicyQuery, PolicyView>
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;
        private readonly IQueryHandler<GetSinglePolicyFromContextQuery, PolicyView> _queryHandler;

        public PolicyQueryHandler(IPolicyEventContextIdQuery policyEventContextIdQuery, IQueryHandler<GetSinglePolicyFromContextQuery, PolicyView> queryHandler)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
            _queryHandler = queryHandler;
        }

        public PolicyView Read(GetSinglePolicyQuery query)
        {
            var policyNumber = query.PolicyNumber;
            var contextId = _policyEventContextIdQuery.GeteventContextId(policyNumber);
            return !contextId.HasValue
                ? null
                : _queryHandler.Read(new GetSinglePolicyFromContextQuery(contextId.Value));
        }
    }

    //public class CustomerPolicyQueryHandler : IQueryHandler<GetCustomerSinglePolicyQuery, PolicyView>
    //{
    //    private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;
    //    private readonly IQueryDispatcher<GetSinglePolicyFromContextQuery> _queryDispatcher;

    //    public CustomerPolicyQueryHandler(IPolicyEventContextIdQuery policyEventContextIdQuery, IQueryDispatcher<GetSinglePolicyFromContextQuery> queryDispatcher)
    //    {
    //        _policyEventContextIdQuery = policyEventContextIdQuery;
    //        _queryDispatcher = queryDispatcher;
    //    }

    //    public IEnumerable<PolicyView> Read(GetCustomerSinglePolicyQuery query)
    //    {
    //        var contextIds = _policyEventContextIdQuery.GeteventContextId(customerId);
    //        return contextIds.Select(contextId => _singlePolicyQueryHandler.Read(contextId));
    //    }
    //}
}