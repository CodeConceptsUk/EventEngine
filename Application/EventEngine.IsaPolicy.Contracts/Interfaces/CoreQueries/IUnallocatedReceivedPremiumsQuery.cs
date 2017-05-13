using System;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces
{
    public interface IUnallocatedReceivedPremiumsQuery : IQuery
    {
        UnallocatedReceivedPremiumsView Read(Guid contextId);
    }
}