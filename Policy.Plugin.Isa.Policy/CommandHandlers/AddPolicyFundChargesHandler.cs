using System.Collections.Generic;
using System.Linq;
using Policy.Application.Exceptions;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Commands;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;

namespace Policy.Plugin.Isa.Policy.CommandHandlers
{
    public class AddPolicyFundChargesHandler : ICommandHandler<AddPolicyFundChargesCommand>
    {
        private readonly IPolicyeventContextIdQuery _policyeventContextIdQuery;
        private readonly IPolicyQuery _policyQuery;

        public AddPolicyFundChargesHandler(IPolicyeventContextIdQuery policyeventContextIdQuery, IPolicyQuery policyQuery)
        {
            _policyeventContextIdQuery = policyeventContextIdQuery;
            _policyQuery = policyQuery;
        }
        
        public IEnumerable<IEvent> Execute(AddPolicyFundChargesCommand command)
        {
            // Fund 1, 2 have charges
            var eventContextId =  _policyeventContextIdQuery.GeteventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");

            //var policy = _policyQuery.Read(command.PolicyNumber);
            //return policy.Funds.Where(FundHasCharging).Select(fund =>
            //{
            //    var units = CalculateFundDeduction(fund);
            //    return new AppliedFundChargeEvent(eventContextId.Value, fund.FundId, units);
            //});
            return null;
        }

        private static decimal CalculateFundDeduction(IFund fund)
        {
            // 3% APR ish
            return -(fund.Units / 100 * 0.0025m);
        }

        private static bool FundHasCharging(IFund fund)
        {
            return fund.FundId == "fund1" || 
                   fund.FundId == "fund2";
        }
    }
}
