using System;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalanceView;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries
{
    public interface IPolicyFundUnitBalanceQuery
    {
        PolicyFundUnitBalanceView Read(Guid contextId);
    }
}