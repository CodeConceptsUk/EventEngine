using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyTransactionsView
{
    public class PolicyTransactionView : IsaPolicyView
    {
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
