using System.Collections.Generic;
using System.Linq;
using Policy.Application.Exceptions;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;
using Policy.Plugin.Isa.Policy.Operations.Commands;
using Policy.Plugin.Isa.Policy.Views.Queries;

namespace Policy.Plugin.Isa.Policy.Operations.CommandHandlers
{
    public class AddPremiumHandler : IsaPolicyCommandHandler<AddPremiumCommand>
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;

        public AddPremiumHandler(IPolicyEventContextIdQuery policyEventContextIdQuery)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
        }

        public override IEnumerable<IsaPolicyEvent> Execute(AddPremiumCommand command)
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