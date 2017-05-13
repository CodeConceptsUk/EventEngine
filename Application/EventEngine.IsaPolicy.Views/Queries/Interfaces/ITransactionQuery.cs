using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces
{
    public interface ITransactionQuery : IQuery
    {
        PolicyTransactionView Read(string policyNumber);
    }
}