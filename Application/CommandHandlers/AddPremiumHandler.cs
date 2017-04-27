using System.Collections.Generic;
using Application.Commands;
using Application.Events;
using Application.Interfaces;
using Application.Interfaces.Domain;
using Application.Interfaces.Queries;

namespace Application.CommandHandlers
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