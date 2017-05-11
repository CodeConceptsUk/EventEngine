using System;
using System.Collections.Generic;
using Policy.Application.Exceptions;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;
using Policy.Plugin.Isa.Policy.Operations.Commands;
using Policy.Plugin.Isa.Policy.Views.Queries;

namespace Policy.Plugin.Isa.Policy.Operations.CommandHandlers
{
    public class AddUnitsToFundHandler : IsaPolicyCommandHandler<AddUnitsToFundCommand>
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;

        public AddUnitsToFundHandler(IPolicyEventContextIdQuery policyEventContextIdQuery)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
        }

        public override IEnumerable<IsaPolicyEvent> Execute(AddUnitsToFundCommand command)
        {
            var eventContextId = _policyEventContextIdQuery.GeteventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");

            return new[] { new UnitsAddedToFundEvent(eventContextId.Value, Guid.NewGuid(), command.FundId, command.Units, command.DateTimeAdded) };
        }
    }
}