using System.Collections.Generic;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Commands;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;

namespace Policy.Plugin.Isa.Policy.CommandHandlers
{
    public class AddPremiumHandler : ICommandHandler<AddPremiumCommand, IPolicyContext>
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;

        public AddPremiumHandler(IPolicyEventContextIdQuery policyEventContextIdQuery)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
        }

        public IEnumerable<IEvent<IPolicyContext>> Execute(AddPremiumCommand command)
        {
            var eventContextId = _policyEventContextIdQuery.Read(command.PolicyNumber).EventContextId;

            return new IEvent<IPolicyContext>[]
            {
                new AddPremiumEvent(
                    eventContextId,
                    command.FundPremiumDetails.FundId,
                    command.FundPremiumDetails.Premium)
            };
        }
    }
}