using System;
using System.Collections.Generic;
using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers;
using CodeConcepts.FrameworkExtensions.LinqExtensions;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    public class CreateChargesHandler : IsaPolicyNumberHandler, ICommandHandler<CreateChargeCommand, IsaPolicyEvent>
    {
        private readonly IPolicyFundUnitBalanceQueryHandler _policyFundUnitBalanceQueryHandler;

        public CreateChargesHandler(IGetEventContextIdForPolicyNumberQueryHandler getEventContextIdForPolicyNumberQueryHandler,
                                    IPolicyFundUnitBalanceQueryHandler policyFundUnitBalanceQueryHandler)
            : base(getEventContextIdForPolicyNumberQueryHandler)
        {
            _policyFundUnitBalanceQueryHandler = policyFundUnitBalanceQueryHandler;
        }

        public IEnumerable<IsaPolicyEvent> Execute(CreateChargeCommand command)
        {
            // Fund 1, 2 have charges
            var eventContextId = GetEventContextId(command.PolicyNumber);

            var policy = _policyFundUnitBalanceQueryHandler.Read(new GetPolicyFundUnitBalanceQuery(eventContextId));
            var events = new List<IsaPolicyEvent>();

            var portion = policy.FundAllocations.SingleOrDefault(f => f.FundId == command.FundId && f.PortionId == command.PortionId && f.UnitBalance > 0);
            // ReSharper disable once InvertIf
            if (portion != null)
            {
                var deductedUnits = CalculateFundDeduction(portion.UnitBalance);
                //Console.WriteLine(portion.UnitBalance);
                events.Add(new AppliedFundChargeEvent(eventContextId, portion.FundId, portion.PortionId, deductedUnits, command.ChargeDate));
            }

            return events;
        }

        private static decimal CalculateFundDeduction(decimal units)
        {
            // 3% APR ish
            // TODO: PRODUCT RULE
            return -Math.Round(units / 100m * 0.0025m, 10);
        }
    }
}
