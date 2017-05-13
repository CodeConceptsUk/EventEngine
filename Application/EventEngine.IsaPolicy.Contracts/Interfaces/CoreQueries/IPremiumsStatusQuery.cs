using System;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatusView;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries
{
    public interface IPremiumsStatusQuery : IQuery
    {
        PremiumsStatusView Read(Guid contextId);
    }
}