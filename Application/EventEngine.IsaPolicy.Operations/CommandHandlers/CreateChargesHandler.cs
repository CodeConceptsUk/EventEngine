using System;
using System.Collections.Generic;
using System.Linq;
using CodeConcepts.EventEngine.Contracts.Exceptions;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.EventContextId;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers;
using CodeConcepts.FrameworkExtensions.LinqExtensions;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    public class CreateChargesHandler : ICommandHandler<CreateChargesCommand, IsaPolicyEvent>
    {
        private readonly IGetEventContextIdForPolicyNumberQueryHandler _getEventContextIdForPolicyNumberQueryHandler;
        private readonly IPolicyFundUnitBalanceQueryHandler _policyFundUnitBalanceQueryHandler;

        public CreateChargesHandler(IGetEventContextIdForPolicyNumberQueryHandler getEventContextIdForPolicyNumberQueryHandler, 
                                    IPolicyFundUnitBalanceQueryHandler policyFundUnitBalanceQueryHandler)
        {
            _getEventContextIdForPolicyNumberQueryHandler = getEventContextIdForPolicyNumberQueryHandler;
            _policyFundUnitBalanceQueryHandler = policyFundUnitBalanceQueryHandler;
        }

        public IEnumerable<IsaPolicyEvent> Execute(CreateChargesCommand command)
        {
            // Fund 1, 2 have charges
            var eventContextId = _getEventContextIdForPolicyNumberQueryHandler.Read(new GetEventContextIdForPolicyNumberQuery(command.PolicyNumber))?.EventContextId;
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");

            var policy = _policyFundUnitBalanceQueryHandler.Read(new GetPolicyFundUnitBalanceQuery(eventContextId.Value));
            var events = new List<IsaPolicyEvent>();

            policy.FundAllocations.Where(allocation => allocation.UnitBalance > 0).ForEach(allocation =>
            {
                var deductedUnits = CalculateFundDeduction(allocation.UnitBalance);
                events.Add(new AppliedFundChargeEvent(eventContextId.Value, allocation.FundId, allocation.PortionId, deductedUnits, command.ChargeDate));
            }); ;

            return events;
        }

        private static decimal CalculateFundDeduction(decimal fund)
        {
            // 3% APR ish
            // TODO: PRODUCT RULE
            return -Math.Round(fund / 100m * 0.0025m, 6);
        }

    }

}
