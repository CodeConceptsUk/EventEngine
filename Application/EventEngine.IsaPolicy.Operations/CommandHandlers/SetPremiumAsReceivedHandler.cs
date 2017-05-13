using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Exceptions;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatusView;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    public class SetPremiumAsReceivedHandler : ICommandHandler<SetPremiumAsReceivedCommand, IsaPolicyEvent>
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;
        private readonly IPremiumsStatusQuery _premiumsStatusQuery;

        public SetPremiumAsReceivedHandler(IPolicyEventContextIdQuery policyEventContextIdQuery, IPremiumsStatusQuery premiumsStatusQuery)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
            _premiumsStatusQuery = premiumsStatusQuery;
        }

        public IEnumerable<IsaPolicyEvent> Execute(SetPremiumAsReceivedCommand command)
        {
            var eventContextId = _policyEventContextIdQuery.GeteventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");
            var premiumStatuses = _premiumsStatusQuery.Read(eventContextId.Value);

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