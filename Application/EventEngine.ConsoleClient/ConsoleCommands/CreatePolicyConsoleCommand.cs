using CliConsole;
using Policy.Plugin.Isa.Policy.Operations.Commands;
using Program.Services;

namespace Program.ConsoleCommands
{
    public class CreatePolicyConsoleCommand : InlineConsoleCommand
    {
        private readonly ICommandChannelClientFactory _commandChannelClientFactory;
        private int _customerId;

        public CreatePolicyConsoleCommand(ICommandChannelClientFactory commandChannelClientFactory)
            : base("CreatePolicy", "Creates a new policy within the system")
        {
            _commandChannelClientFactory = commandChannelClientFactory;
            HasRequiredOption<int>("customerId", "CustomerId that the policy will belong to:", p => _customerId = p);
        }

        protected override void Execute()
        {
            var client = _commandChannelClientFactory.Create();
            client.DispatchCommand(new CreatePolicyCommand(_customerId));
        }
    }
}