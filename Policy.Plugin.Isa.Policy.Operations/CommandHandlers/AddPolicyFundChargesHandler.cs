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
    public class AddPolicyFundChargesHandler : IsaPolicyCommandHandler<AddPolicyFundChargesCommand>
    {
        private readonly IPolicyEventContextIdQuery _policyeventContextIdQuery;
        private readonly IPolicyQuery _policyQuery;

        public AddPolicyFundChargesHandler(IPolicyEventContextIdQuery policyeventContextIdQuery, IPolicyQuery policyQuery)
        {
            _policyeventContextIdQuery = policyeventContextIdQuery;
            _policyQuery = policyQuery;
        }
        
        public override IEnumerable<IsaPolicyEvent> Execute(AddPolicyFundChargesCommand command)
        {
            // Fund 1, 2 have charges
            var eventContextId =  _policyeventContextIdQuery.GeteventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");

            var policy = _policyQuery.Read(command.PolicyNumber);
            var events = new List<IsaPolicyEvent>();

            policy.Funds.ForEach(fund =>
            {
                fund.Allocations.Where(allocation => allocation.Units>0).ForEach(allocation =>
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
            return -(fund / 100m * 0.0025m);
        }

    }
}
