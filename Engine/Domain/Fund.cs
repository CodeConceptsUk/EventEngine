using System.Collections.Generic;
using Engine.Commands;
using Engine.Interfaces;

namespace Engine.Domain
{
    public class Policy : IPolicy
    {
        private readonly ICommandBus _commandBus;

        public Policy(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public string PolicyNumber { get; }

        public IEnumerable<IFund> Funds { get; }

        public void AddCharge(IFund fund, decimal units)
        {
            var command = new AddCharge(PolicyNumber, fund.FundId, units);
            _commandBus.Publish(command);
        }
    }

    public class Fund : IFund
    {
        public string FundId { get; }

        public decimal Value { get; }
    }
}