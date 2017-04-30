using System;
using System.Collections.Generic;
using System.Linq;
using Policy.Application.Exceptions;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Commands;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;
using Policy.Plugin.Isa.Policy.Views.PolicyView;

namespace Policy.Plugin.Isa.Policy.CommandHandlers
{
    public class UnitAllocationHandler : ICommandHandler<UnitAllocationCommand>
    {
        private readonly IPolicyeventContextIdQuery _policyeventContextIdQuery;
        private readonly IPolicyQuery _policyQuery;
        private readonly IUnitPricingRepository _unitPricingRepository;

        public UnitAllocationHandler(IPolicyeventContextIdQuery policyeventContextIdQuery, IPolicyQuery policyQuery, IUnitPricingRepository unitPricingRepository)
        {
            _policyeventContextIdQuery = policyeventContextIdQuery;
            _policyQuery = policyQuery;
            _unitPricingRepository = unitPricingRepository;
        }

        public IEnumerable<IEvent> Execute(UnitAllocationCommand command)
        {
            var policy = _policyQuery.Read(command.PolicyNumber);
            var eventContextId = _policyeventContextIdQuery.GeteventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");

            return CreateUnitAllocationEventsForPolicyFunds(command, policy, eventContextId);
        }

        private IEnumerable<IEvent> CreateUnitAllocationEventsForPolicyFunds(UnitAllocationCommand command, PolicyView policy, Guid? eventContextId)
        {
            return policy.Funds.Where(fund => fund.UnallocatedPremiums != 0).Select(fund =>
            {
                var units = _unitPricingRepository.Get(fund.FundId, command.DateOfAllocation, fund.UnallocatedPremiums);
                return new UnitsAllocatedEvent(eventContextId.Value, fund.FundId, units, fund.UnallocatedPremiums,
                    command.DateOfAllocation);
            });
        }
    }
}