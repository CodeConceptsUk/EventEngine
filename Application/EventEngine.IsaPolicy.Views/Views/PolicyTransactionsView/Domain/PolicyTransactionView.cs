using System.Collections.Generic;
using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.Domain
{
    public class PolicyTransactionView : IView
    {
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
