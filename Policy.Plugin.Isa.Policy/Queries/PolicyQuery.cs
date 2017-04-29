using System.Collections.Generic;
using System.Linq;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;
using Policy.Plugin.Isa.Policy.Views.PolicyView;

namespace Policy.Plugin.Isa.Policy.Queries
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
            var contextId = _policyEventContextIdQuery.GetEventContextId(policyNumber);
            return !contextId.HasValue 
                ? null 
                : _singlePolicyQuery.Build(contextId.Value);
        }

        public IEnumerable<PolicyView> Read(int customerId)
        {
            var contextIds = _policyEventContextIdQuery.GetEventContextId(customerId);
            return contextIds.Select(contextId => _singlePolicyQuery.Build(contextId));
        }
    }
}