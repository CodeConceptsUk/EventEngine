using Policy.Plugin.Isa.Policy.Views.Queries;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.Domain;

namespace Policy.Plugin.Isa.Policy.Views.Views.AggregateQueries
{
    public class TransactionQuery : ITransactionQuery
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;
        private readonly ISinglePolicyTransactionQuery _singlePolicyTransactionQuery;

        public TransactionQuery(IPolicyEventContextIdQuery policyEventContextIdQuery, ISinglePolicyTransactionQuery singlePolicyTransactionQuery)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
            _singlePolicyTransactionQuery = singlePolicyTransactionQuery;
        }

        public PolicyTransactionView Read(string policyNumber)
        {
            var contextId = _policyEventContextIdQuery.GeteventContextId(policyNumber);
            return !contextId.HasValue
                ? null
                : _singlePolicyTransactionQuery.Read(contextId.Value);
        }
    }
}