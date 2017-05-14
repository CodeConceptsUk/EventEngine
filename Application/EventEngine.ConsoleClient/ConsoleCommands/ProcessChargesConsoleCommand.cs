using CodeConcepts.CliConsole;
using CodeConcepts.EventEngine.ClientLibrary.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;

namespace CodeConcepts.EventEngine.ConsoleClient.ConsoleCommands
{
    public class ProcessChargesConsoleCommand : InlineConsoleCommand
    {
        private readonly ICommandChannelClientFactory _commandChannelClientFactory;
        private string _policyNumber;

        public ProcessChargesConsoleCommand(ICommandChannelClientFactory commandChannelClientFactory)
            : base("ProcessCharges", "Backdate all charges not yet added to a policy")
        {
            _commandChannelClientFactory = commandChannelClientFactory;

            HasRequiredOption<string>("PolicyNumber", "The policy number", p => _policyNumber = p);
        }

        protected override void Execute()
        {
            var processChargesCommand = new ProcessChargesCommand(_policyNumber);

            var client = _commandChannelClientFactory.Create();
            client.DispatchCommand(processChargesCommand);
        }
    }
}