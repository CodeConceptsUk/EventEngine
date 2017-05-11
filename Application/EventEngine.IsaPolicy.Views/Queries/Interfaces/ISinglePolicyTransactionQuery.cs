using System;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.Domain;

namespace Policy.Plugin.Isa.Policy.Views.Queries
{
    public interface ISinglePolicyTransactionQuery
    {
        PolicyTransactionView Read(Guid contextId);
    }
}