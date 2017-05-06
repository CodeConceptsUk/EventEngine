using System;
using System.Collections.Generic;
using System.Linq;
using FrameworkExtensions.LinqExtensions;
using Policy.Application.Exceptions;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;
using Policy.Plugin.Isa.Policy.Operations.Commands;
using Policy.Plugin.Isa.Policy.Views.Queries;

namespace Policy.Plugin.Isa.Policy.Operations.CommandHandlers
{
    public class CreateChargesHandler : IsaPolicyCommandHandler<CreateChargesCommand>
    {
        private readonly IPolicyEventContextIdQuery _policyeventContextIdQuery;
        private readonly ISinglePolicyQuery _singlePolicyQuery;

        public CreateChargesHandler(IPolicyEventContextIdQuery policyeventContextIdQuery, ISinglePolicyQuery singlePolicyQuery)
        {
            _policyeventContextIdQuery = policyeventContextIdQuery;
            _singlePolicyQuery = singlePolicyQuery;
        }

        public override IEnumerable<IsaPolicyEvent> Execute(CreateChargesCommand command)
        {
            // Fund 1, 2 have charges
            var eventContextId = _policyeventContextIdQuery.GeteventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");

            var policy = _singlePolicyQuery.Build(eventContextId.Value);
            var events = new List<IsaPolicyEvent>();

            policy.Funds.ForEach(fund =>
            {
                fund.Allocations.Where(allocation => allocation.Units > 0).ForEach(allocation =>
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
