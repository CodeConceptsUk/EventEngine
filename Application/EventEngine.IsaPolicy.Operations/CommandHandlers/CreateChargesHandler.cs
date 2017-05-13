using System;
using System.Collections.Generic;
using System.Linq;
using CodeConcepts.EventEngine.Contracts.Exceptions;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Events.Events;
using CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyView.Domain;
using CodeConcepts.FrameworkExtensions.LinqExtensions;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    public class CreateChargesHandler : ICommandHandler<CreateChargesCommand, IsaPolicyEvent>
    {
        private readonly IPolicyEventContextIdQuery _policyeventContextIdQuery;
        private readonly ISinglePolicyQuery _singlePolicyQuery;

        public CreateChargesHandler(IPolicyEventContextIdQuery policyeventContextIdQuery, ISinglePolicyQuery singlePolicyQuery)
        {
            _policyeventContextIdQuery = policyeventContextIdQuery;
            _singlePolicyQuery = singlePolicyQuery;
        }

        public IEnumerable<IsaPolicyEvent> Execute(CreateChargesCommand command)
        {
            // Fund 1, 2 have charges
            var eventContextId = _policyeventContextIdQuery.GeteventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");

            var policy = _singlePolicyQuery.Read(eventContextId.Value);
            var events = new List<IsaPolicyEvent>();

            policy.Funds.ForEach(fund =>
            {
                Enumerable.Where<FundAllocation>(fund.Allocations, allocation => allocation.Units > 0).ForEach(allocation =>
                  {
                      var deductedUnits = CalculateFundDeduction(allocation.Units);
                      events.Add(new AppliedFundChargeEvent(eventContextId.Value, fund.FundId, allocation.PortionId, deductedUnits, command.ChargeDate));
                  });
            });

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
