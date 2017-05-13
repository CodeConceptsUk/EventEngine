using System;
using CodeConcepts.CliConsole;
using CodeConcepts.EventEngine.ClientLibrary.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.ConsoleClient.ConsoleCommands
{
    public class CreatePolicyConsoleCommand : InlineConsoleCommand
    {
        private readonly ICommandChannelClientFactory _commandChannelClientFactory;
        private readonly ISequencingRepository _sequencingRepository;
        private int _customerId;

        public CreatePolicyConsoleCommand(ICommandChannelClientFactory commandChannelClientFactory, ISequencingRepository sequencingRepository)
            : base("CreatePolicy", "Creates a new policy within the system")
        {
            _sequencingRepository = sequencingRepository;
            _commandChannelClientFactory = commandChannelClientFactory;
            HasRequiredOption<int>("customerId", "CustomerId that the policy will belong to:", p => _customerId = p);
        }

        protected override void Execute()
        {
            //TODO consider this:
            // have moved specification of the policy number here in addition to creation of the context id.
            // specifying the contextId here allows all commands to have no return values. - this is good.
            // I am less certain that specifying policy number here is the best approach. We could alternatively
            // derive it in an event handler (based on previous create policy events). or we could put it back
            // where it was and it would work fine but there would be (tentative) reason to return it from the
            // command (although you could execute the command and then immediately query for the policy using
            // the context id which would achieve the same result)
            // one other option I am considering is that perhaps the allocation of numbers is an api/service 
            // method provided by either the eventEngine itself or the isa plugin
            var policyNumber = _sequencingRepository.Get("IsaPolicyPolicyNumber");
            var client = _commandChannelClientFactory.Create();
            client.DispatchCommand(new CreatePolicyCommand(Guid.NewGuid() ,_customerId, policyNumber));
        }
    }
}