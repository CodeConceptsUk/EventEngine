using System;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces
{
    public interface ISinglePolicyTransactionQuery
    {
        PolicyTransactionView Read(Guid contextId);
    }
}