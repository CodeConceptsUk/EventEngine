using System;
using CodeConcepts.EventEngine.Application.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces
{
    public interface ISinglePolicyQuery : IQuery
    {
        PolicyView Read(Guid contextId);
    }
}