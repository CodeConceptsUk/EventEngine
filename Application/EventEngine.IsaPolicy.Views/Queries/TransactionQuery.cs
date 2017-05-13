//using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;
//using CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces;
//using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.Domain;

//namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries
//{
//    public class TransactionQuery : ITransactionQuery
//    {
//        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;
//        private readonly ISinglePolicyTransactionQuery _singlePolicyTransactionQuery;

//        public TransactionQuery(IPolicyEventContextIdQuery policyEventContextIdQuery, ISinglePolicyTransactionQuery singlePolicyTransactionQuery)
//        {
//            _policyEventContextIdQuery = policyEventContextIdQuery;
//            _singlePolicyTransactionQuery = singlePolicyTransactionQuery;
//        }

//        public PolicyTransactionView Read(string policyNumber)
//        {
//            var contextId = _policyEventContextIdQuery.GeteventContextId(policyNumber);
//            return !contextId.HasValue
//                ? null
//                : _singlePolicyTransactionQuery.Read(contextId.Value);
//        }
//    }
//}