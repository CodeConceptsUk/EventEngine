using System;
using CodeConcepts.EventEngine.Contracts.Exceptions;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    public abstract class IsaPolicyNumberHandler
    {
        private readonly IGetEventContextIdForPolicyNumberQueryHandler _getEventContextIdForPolicyNumberQueryHandler;

        protected IsaPolicyNumberHandler(IGetEventContextIdForPolicyNumberQueryHandler getEventContextIdForPolicyNumberQueryHandler)
        {
            _getEventContextIdForPolicyNumberQueryHandler = getEventContextIdForPolicyNumberQueryHandler;
        }

        protected Guid GetEventContextId(string policyNumber)
        {
            var eventContextId = _getEventContextIdForPolicyNumberQueryHandler.Read(new GetEventContextIdForPolicyNumberQuery(policyNumber))?.EventContextId;
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {policyNumber} does not exist!");

            return eventContextId.Value;
        }
    }
}