using Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.Domain;

namespace Policy.Plugin.Isa.Policy.Views.Queries
{
    public interface ITransactionQuery
    {
        PolicyTransactionView Read(string policyNumber);
    }
}