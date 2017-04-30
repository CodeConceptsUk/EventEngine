using System.Collections.Generic;
using Policy.Application.Exceptions;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Commands;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;

namespace Policy.Plugin.Isa.Policy.CommandHandlers
{
    public class AddPremiumHandler : ICommandHandler<AddPremiumCommand>
    {
        private readonly IPolicyeventContextIdQuery _policyeventContextIdQuery;

        public AddPremiumHandler(IPolicyeventContextIdQuery policyeventContextIdQuery)
        {
            _policyeventContextIdQuery = policyeventContextIdQuery;
        }

        public IEnumerable<IEvent> Execute(AddPremiumCommand command)
        {
            var eventContextId = _policyeventContextIdQuery.GeteventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");

            return new IEvent[]
            {
                new AddPremiumEvent(
                    eventContextId.Value,
                    command.FundPremiumDetails.FundId,
                    command.FundPremiumDetails.Premium,
                    command.PremiumDateTime)
            };
        }
    }
}