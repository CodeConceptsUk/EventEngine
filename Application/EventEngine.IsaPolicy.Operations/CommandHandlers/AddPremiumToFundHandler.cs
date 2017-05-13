using System.Collections.Generic;
using System.Linq;
using CodeConcepts.EventEngine.Contracts.Exceptions;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Events.Events;
using CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    public class AddPremiumHandler : ICommandHandler<AddPremiumCommand, IsaPolicyEvent>
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;

        public AddPremiumHandler(IPolicyEventContextIdQuery policyEventContextIdQuery)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
        }

        public IEnumerable<IsaPolicyEvent> Execute(AddPremiumCommand command)
        {
            var eventContextId = _policyEventContextIdQuery.GeteventContextId(command.PolicyNumber);
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