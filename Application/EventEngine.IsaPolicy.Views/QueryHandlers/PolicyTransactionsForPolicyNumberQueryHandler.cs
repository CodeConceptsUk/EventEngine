using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyTransactionsView;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.QueryHandlers
{
    public class PolicyTransactionsForPolicyNumberQueryHandler : IQueryHandler<GetPolicyTransactionsForPolicyNumberQuery, PolicyTransactionView>
    {
        private readonly IGetEventContextIdForPolicyNumberQueryHandler _getEventContextIdForPolicyNumberQueryHandler;
        private readonly IQueryHandler<GetPolicyTransactionsForEventContextIdQuery, PolicyTransactionView> _singlePolicyTransactionQuery;

        public PolicyTransactionsForPolicyNumberQueryHandler(IGetEventContextIdForPolicyNumberQueryHandler getEventContextIdForPolicyNumberQueryHandler, IQueryHandler<GetPolicyTransactionsForEventContextIdQuery, PolicyTransactionView> singlePolicyTransactionQuery)
        {
            _getEventContextIdForPolicyNumberQueryHandler = getEventContextIdForPolicyNumberQueryHandler;
            _singlePolicyTransactionQuery = singlePolicyTransactionQuery;
        }

        public PolicyTransactionView Read(GetPolicyTransactionsForPolicyNumberQuery getPolicyTransactionsForPolicyNumberQuery)
        {
            var contextId = _getEventContextIdForPolicyNumberQueryHandler
                        .Read(new GetEventContextIdForPolicyNumberQuery(getPolicyTransactionsForPolicyNumberQuery.PolicyNumber))?.EventContextId;
            return !contextId.HasValue
                ? null
                : _singlePolicyTransactionQuery.Read(new GetPolicyTransactionsForEventContextIdQuery(contextId.Value));
        }
    }
}