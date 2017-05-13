using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.Domain
{
    public class PolicyTransactionView : IView
    {
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
