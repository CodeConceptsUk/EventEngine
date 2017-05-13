using System;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiumsView;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries
{
    public interface IUnallocatedReceivedPremiumsQuery : IQuery
    {
        UnallocatedReceivedPremiumsView Read(Guid contextId);
    }
}