using System;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces
{
    public interface ISomethingSomethingQuery : IQuery
    {
        PendingPremiumView Read(Guid contextId, string commandPremiumId);
    }
}