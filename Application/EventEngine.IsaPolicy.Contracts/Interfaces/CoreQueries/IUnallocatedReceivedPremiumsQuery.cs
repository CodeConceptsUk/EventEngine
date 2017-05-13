using System;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiumsView;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries
{
    public interface IUnallocatedReceivedPremiumsQuery : IQuery
    {
        UnallocatedReceivedPremiumsView Read(Guid contextId);
    }
}