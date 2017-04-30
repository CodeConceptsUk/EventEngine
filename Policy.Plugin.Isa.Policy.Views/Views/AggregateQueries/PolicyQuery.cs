using System.Collections.Generic;
using System.Linq;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;

namespace Policy.Plugin.Isa.Policy.Views.Views.AggregateQueries
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

        public PolicyView.Domain.PolicyView Read(string policyNumber)
        {
            var contextId = _policyEventContextIdQuery.GeteventContextId(policyNumber);
            return !contextId.HasValue
                ? null
                : _singlePolicyQuery.Build(contextId.Value);
        }

        public IEnumerable<PolicyView.Domain.PolicyView> Read(int customerId)
        {
            var contextIds = _policyEventContextIdQuery.GeteventContextId(customerId);
            return contextIds.Select(contextId => _singlePolicyQuery.Build(contextId));
        }
    }
}