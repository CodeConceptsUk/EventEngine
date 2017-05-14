using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalance;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers
{
    public interface IPolicyFundUnitBalanceQueryHandler : IQueryHandler<GetPolicyFundUnitBalanceQuery, PolicyFundUnitBalanceView>
    {
        
    }
}