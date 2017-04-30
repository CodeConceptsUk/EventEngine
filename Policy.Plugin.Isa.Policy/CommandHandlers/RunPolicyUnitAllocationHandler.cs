using System;
using System.Collections.Generic;
using System.Linq;
using Policy.Application.Exceptions;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Commands;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;
using Policy.Plugin.Isa.Policy.Views.PolicyView;

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
            var policy = _policyQuery.Read(command.PolicyNumber);
            var eventContextId = _policyEventContextIdQuery.GetEventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");

            return CreateUnitAllocationEventsForPolicyFunds(command, policy, eventContextId);
        }

        private IEnumerable<IEvent<IPolicyContext>> CreateUnitAllocationEventsForPolicyFunds(UnitAllocationCommand command, PolicyView policy, Guid? eventContextId)
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