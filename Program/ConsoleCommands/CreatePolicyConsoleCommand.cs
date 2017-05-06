using CliConsole;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;
using Policy.Plugin.Isa.Policy.Operations.Commands;

namespace Program.ConsoleCommands
{
    public class CreatePolicyConsoleCommand : InlineConsoleCommand
    {
        private readonly ICommandDispatcher<IsaPolicyCommand> _dispatcher;
        private int _customerId;

        public CreatePolicyConsoleCommand(ICommandDispatcher<IsaPolicyCommand> dispatcher)
            : base("CreatePolicy", "Creates a new policy within the system")
        {
            _dispatcher = dispatcher;
            HasRequiredOption<int>("customerId", "CustomerId that the policy will belong to:", p => _customerId = p);
        }

        protected override void Execute()
        {
            var command = new CreatePolicyCommand(_customerId);
            _dispatcher.Apply(command);
        }
    }
}