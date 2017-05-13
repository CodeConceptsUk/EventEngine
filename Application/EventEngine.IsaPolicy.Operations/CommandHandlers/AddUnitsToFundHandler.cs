using System;
using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Exceptions;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    public class AddUnitsToFundHandler : ICommandHandler<AddUnitsToFundCommand, IsaPolicyEvent>
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;

        public AddUnitsToFundHandler(IPolicyEventContextIdQuery policyEventContextIdQuery)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
        }

        public IEnumerable<IsaPolicyEvent> Execute(AddUnitsToFundCommand command)
        {
            var eventContextId = _policyEventContextIdQuery.GeteventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");

            return new[] { new UnitsAddedToFundEvent(eventContextId.Value, Guid.NewGuid(), command.FundId, command.Units, command.DateTimeAdded) };
        }
    }
}