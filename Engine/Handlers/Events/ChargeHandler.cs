using System.Linq;
using Engine.Interfaces.Repositories;

namespace Engine.Handlers.Events
{
    public class ChargeHandler : IHandler<ChargeApplied>
    {
        private readonly IPolicyRepository _repository;

        public ChargeHandler(IPolicyRepository repository)
        {
            _repository = repository;
        }

        public void Handle(ChargeApplied @event)
        {
            var policy = _repository.Get(@event.PolicyNumber);
            var fund = policy.Funds.First(f => f.FundId == @event.Fundid);
        }
    }
}
