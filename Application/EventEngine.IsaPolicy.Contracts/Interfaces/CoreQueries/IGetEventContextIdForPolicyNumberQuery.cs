using System;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries
{
    public interface IGetEventContextIdForPolicyNumberQuery
    {
        Guid? GetEventContextId(string policyNumber);
    }
}