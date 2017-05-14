using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyTransactionsView
{
    public class PolicyTransactionView : IView
    {
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
