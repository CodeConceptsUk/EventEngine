using System;
using CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueries
{
    public class SomethingSomethingQuery : ISomethingSomethingQuery
    {
        public PendingPremiumView Read(Guid contextId, string commandPremiumId)
        {
            //var premium = policy.PendingPremiums.Single(p => p.PremiumId == command.PremiumId);
            //if (premium.IsAllocated)
            //    throw new PolicyException($"Premium has already been allocated");
            //if (premium.IsReceived)
            //    throw new PolicyException($"Premium has already been received");
            throw new NotImplementedException();
        }
    }
}