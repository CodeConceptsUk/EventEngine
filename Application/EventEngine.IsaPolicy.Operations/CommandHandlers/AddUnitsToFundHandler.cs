using System;
using System.Collections.Generic;
using CodeConcepts.EventEngine.Application.Exceptions;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Events.Events;
using CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
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