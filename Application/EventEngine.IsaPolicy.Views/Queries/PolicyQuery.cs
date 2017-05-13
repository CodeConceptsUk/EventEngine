using System.Collections.Generic;
using System.Linq;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries
{
    public class PolicyQuery : IPolicyQuery
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;
        private readonly ISinglePolicyQuery _singlePolicyQuery;

        public PolicyQuery(IPolicyEventContextIdQuery policyEventContextIdQuery, ISinglePolicyQuery singlePolicyQuery)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
            _singlePolicyQuery = singlePolicyQuery;
        }

        public PolicyView Read(string policyNumber)
        {
            var contextId = _policyEventContextIdQuery.GeteventContextId(policyNumber);
            return !contextId.HasValue
                ? null
                : _singlePolicyQuery.Read(contextId.Value);
        }

        public IEnumerable<PolicyView> Read(int customerId)
        {
            var contextIds = _policyEventContextIdQuery.GeteventContextId(customerId);
            return contextIds.Select(contextId => _singlePolicyQuery.Read(contextId));
        }
    }
}