using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries
{
    public class PolicyForPolicyNumberQueryHandler : IQueryHandler<GetPolicyForPolicyNumberQuery, PolicyView>
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;
        private readonly IQueryHandler<GetPolicyForContextIdQuery, PolicyView> _queryHandler;

        public PolicyForPolicyNumberQueryHandler(IPolicyEventContextIdQuery policyEventContextIdQuery, IQueryHandler<GetPolicyForContextIdQuery, PolicyView> queryHandler)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
            _queryHandler = queryHandler;
        }

        public PolicyView Read(GetPolicyForPolicyNumberQuery forPolicyNumberQuery)
        {
            var policyNumber = forPolicyNumberQuery.PolicyNumber;
            var contextId = _policyEventContextIdQuery.GeteventContextId(policyNumber);
            return !contextId.HasValue
                ? null
                : _queryHandler.Read(new GetPolicyForContextIdQuery(contextId.Value));
        }
    }
}