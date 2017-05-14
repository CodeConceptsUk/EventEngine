using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Exceptions;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatus;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    public class SetPremiumAsReceivedHandler : ICommandHandler<SetPremiumAsReceivedCommand, IsaPolicyEvent>
    {
        private readonly IGetEventContextIdForPolicyNumberQueryHandler _getEventContextIdForPolicyNumberQueryHandler;
        private readonly IPremiumsStatusQueryHandler _premiumsStatusQueryHandler;

        public SetPremiumAsReceivedHandler(IGetEventContextIdForPolicyNumberQueryHandler getEventContextIdForPolicyNumberQueryHandler, 
                                           IPremiumsStatusQueryHandler premiumsStatusQueryHandler)
        {
            _getEventContextIdForPolicyNumberQueryHandler = getEventContextIdForPolicyNumberQueryHandler;
            _premiumsStatusQueryHandler = premiumsStatusQueryHandler;
        }

        public IEnumerable<IsaPolicyEvent> Execute(SetPremiumAsReceivedCommand command)
        {
            var eventContextId = _getEventContextIdForPolicyNumberQueryHandler.Read(new GetEventContextIdForPolicyNumberQuery(command.PolicyNumber))?.EventContextId;
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");
            var premiumStatuses = _premiumsStatusQueryHandler.Read(new GetPremiumsStatusQuery(eventContextId.Value));

            ValidatePremiumStatus(command.PremiumId, premiumStatuses);

            return new[] { new PremiumReceivedEvent(eventContextId.Value, command.PremiumId, command.DateTimeReceived) };
        }

        private static void ValidatePremiumStatus(string premiumId, PremiumsStatusView premiumStatuses)
        {
            if (premiumStatuses.ReceivedPremiumIds.Contains(premiumId))
                throw new QueryException($"Premium has already been received");
            if (premiumStatuses.AllocatedPremiumIds.Contains(premiumId))
                throw new QueryException($"Premium has already been allocated");
            if (!premiumStatuses.PendingPremiumIds.Contains(premiumId))
                throw new QueryException("Premium was not found on policy!");
        }
    }
}