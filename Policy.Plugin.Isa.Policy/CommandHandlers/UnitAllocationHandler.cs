using System.Collections.Generic;
using System.Linq;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Commands;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;

namespace Policy.Plugin.Isa.Policy.CommandHandlers
{
    public class UnitAllocationHandler : ICommandHandler<UnitAllocationCommand, IPolicyContext>
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;
        private readonly IPolicyQuery _policyQuery;
        private readonly IUnitPricingRepository _unitPricingRepository;

        public UnitAllocationHandler(IPolicyEventContextIdQuery policyEventContextIdQuery, IPolicyQuery policyQuery, IUnitPricingRepository unitPricingRepository)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
            _policyQuery = policyQuery;
            _unitPricingRepository = unitPricingRepository;
        }

        public IEnumerable<IEvent<IPolicyContext>> Execute(UnitAllocationCommand command)
        {
            var policy = _policyQuery.Read(command.PolicyNumber).First();
            var eventContextId = _policyEventContextIdQuery.GetEventContextId(policy.PolicyNumber);

            return policy.Funds.Where(fund => fund.UnallocatedPremiums != 0).Select(fund =>
            {
                var units = _unitPricingRepository.Get(fund.FundId, command.DateOfAllocation, fund.UnallocatedPremiums);
                return new UnitsAllocatedEvent(eventContextId, fund.FundId, units, fund.UnallocatedPremiums, command.DateOfAllocation);
            });
        }
    }
}