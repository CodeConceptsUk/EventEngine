using System.Collections.Generic;
using System.Linq;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;

namespace Policy.Plugin.Isa.Policy.Views.AggregateQueries
{
    public class PolicyQuery : IPolicyQuery
    {
        private readonly IPolicyeventContextIdQuery _policyeventContextIdQuery;
        private readonly ISinglePolicyQuery _singlePolicyQuery;

        public PolicyQuery(IPolicyeventContextIdQuery policyeventContextIdQuery, ISinglePolicyQuery singlePolicyQuery)
        {
            _policyeventContextIdQuery = policyeventContextIdQuery;
            _singlePolicyQuery = singlePolicyQuery;
        }

        public PolicyView.Domain.PolicyView Read(string policyNumber)
        {
            var contextId = _policyeventContextIdQuery.GeteventContextId(policyNumber);
            return !contextId.HasValue
                ? null
                : _singlePolicyQuery.Build(contextId.Value);
        }

        public IEnumerable<PolicyView.Domain.PolicyView> Read(int customerId)
        {
            var contextIds = _policyeventContextIdQuery.GeteventContextId(customerId);
            return contextIds.Select(contextId => _singlePolicyQuery.Build(contextId));
        }
    }
}