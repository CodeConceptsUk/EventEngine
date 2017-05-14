using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.QueryHandlers
{
    public class PolicyForPolicyNumberQueryHandler : IQueryHandler<GetPolicyForPolicyNumberQuery, PolicyView>
    {
        private readonly IGetEventContextIdForPolicyNumberQueryHandler _getEventContextIdForPolicyNumberQueryHandler;
        private readonly IQueryHandler<GetPolicyForContextIdQuery, PolicyView> _queryHandler;

        public PolicyForPolicyNumberQueryHandler(IGetEventContextIdForPolicyNumberQueryHandler getEventContextIdForPolicyNumberQueryHandler, 
                                                IQueryHandler<GetPolicyForContextIdQuery, PolicyView> queryHandler)
        {
            _getEventContextIdForPolicyNumberQueryHandler = getEventContextIdForPolicyNumberQueryHandler;
            _queryHandler = queryHandler;
        }

        public PolicyView Read(GetPolicyForPolicyNumberQuery getPolicyForPolicyNumberQuery)
        {
            var policyNumber = getPolicyForPolicyNumberQuery.PolicyNumber;
            var contextId = _getEventContextIdForPolicyNumberQueryHandler.Read(new GetEventContextIdForPolicyNumberQuery(policyNumber))?.EventContextId;
            return !contextId.HasValue
                ? null
                : _queryHandler.Read(new GetPolicyForContextIdQuery(contextId.Value));
        }
    }
}