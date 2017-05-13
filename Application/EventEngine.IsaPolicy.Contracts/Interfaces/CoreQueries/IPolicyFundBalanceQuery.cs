using System;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundBalanceView;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries
{
    public interface IPolicyFundBalanceQuery : IQuery
    {
        PolicyFundBalanceView Read(Guid contextId);
    }
}