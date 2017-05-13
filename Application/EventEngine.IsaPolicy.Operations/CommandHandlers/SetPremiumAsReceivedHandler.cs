using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using CodeConcepts.EventEngine.Contracts.Exceptions;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Events.Events;
using CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    public class SetPremiumAsReceivedHandler : ICommandHandler<SetPremiumAsReceivedCommand, IsaPolicyEvent>
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;
        private readonly ISomethingSomethingQuery _somethingSomethingQuery;

        public SetPremiumAsReceivedHandler(IPolicyEventContextIdQuery policyEventContextIdQuery, ISomethingSomethingQuery somethingSomethingQuery)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
            _somethingSomethingQuery = somethingSomethingQuery;
        }

        public IEnumerable<IsaPolicyEvent> Execute(SetPremiumAsReceivedCommand command)
        {
            var eventContextId = _policyEventContextIdQuery.GeteventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");
            var premium = _somethingSomethingQuery.Read(eventContextId.Value, command.PremiumId);
            if(premium == null)
                throw new QueryException("Premium was not found on policy!");

            return new[] { new PremiumReceivedEvent(eventContextId.Value, premium.PremiumId, command.DateTimeReceived) };
        }
    }
}