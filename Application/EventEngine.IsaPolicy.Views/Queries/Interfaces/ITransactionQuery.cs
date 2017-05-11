using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces
{
    public interface ITransactionQuery
    {
        PolicyTransactionView Read(string policyNumber);
    }
}