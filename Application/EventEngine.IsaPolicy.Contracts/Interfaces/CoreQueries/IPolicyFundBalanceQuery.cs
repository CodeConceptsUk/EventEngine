using System;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces
{
    public interface IPolicyFundBalanceQuery : IQuery
    {
        PolicyFundBalanceView Read(Guid contextId);
    }
}