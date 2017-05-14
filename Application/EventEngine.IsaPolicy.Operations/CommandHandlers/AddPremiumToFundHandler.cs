using System.Collections.Generic;
using System.Linq;
using CodeConcepts.EventEngine.Contracts.Exceptions;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.EventContextId;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    public class AddPremiumHandler : ICommandHandler<AddPremiumCommand, IsaPolicyEvent>
    {
        private readonly IQueryHandler<GetEventContextIdForPolicyNumberQuery, EventContextIdView> _getEventContextIdForPolicyNumberViewQuery;

        public AddPremiumHandler(IQueryHandler<GetEventContextIdForPolicyNumberQuery, EventContextIdView> getEventContextIdForPolicyNumberViewQuery)
        {
            _getEventContextIdForPolicyNumberViewQuery = getEventContextIdForPolicyNumberViewQuery;
        }

        public IEnumerable<IsaPolicyEvent> Execute(AddPremiumCommand command)
        {
            var eventContextId = _getEventContextIdForPolicyNumberViewQuery.Read(new GetEventContextIdForPolicyNumberQuery(command.PolicyNumber))?.EventContextId;
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");

            return new IsaPolicyEvent[]
            {
                new AddPremiumEvent(
                    eventContextId.Value,
                    command.PremiumId,
                    command.PremiumDateTime,
                    command.FundPremiumDetail.Select(t => new PremiumPartitionDetails { PartitionId = t.PartitionId, Amount =  t.Amount, FundId = t.FundId}).ToList())
            };
        }
    }
}