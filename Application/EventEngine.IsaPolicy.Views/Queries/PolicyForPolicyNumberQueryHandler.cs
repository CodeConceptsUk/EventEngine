using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries
{
    public class PolicyForPolicyNumberQueryHandler : IQueryHandler<GetPolicyForPolicyNumberQuery, PolicyView>
    {
        private readonly IGetEventContextIdForPolicyNumberQuery _getEventContextIdForPolicyNumberQuery;
        private readonly IQueryHandler<GetPolicyForContextIdQuery, PolicyView> _queryHandler;

        public PolicyForPolicyNumberQueryHandler(IGetEventContextIdForPolicyNumberQuery getEventContextIdForPolicyNumberQuery, IQueryHandler<GetPolicyForContextIdQuery, PolicyView> queryHandler)
        {
            _getEventContextIdForPolicyNumberQuery = getEventContextIdForPolicyNumberQuery;
            _queryHandler = queryHandler;
        }

        public PolicyView Read(GetPolicyForPolicyNumberQuery forPolicyNumberQuery)
        {
            var policyNumber = forPolicyNumberQuery.PolicyNumber;
            var contextId = _getEventContextIdForPolicyNumberQuery.GetEventContextId(policyNumber);
            return !contextId.HasValue
                ? null
                : _queryHandler.Read(new GetPolicyForContextIdQuery(contextId.Value));
        }
    }
}