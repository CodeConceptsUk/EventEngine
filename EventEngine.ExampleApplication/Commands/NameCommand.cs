using EventEngine.Application.Interfaces.Commands;

namespace EventEngine.UnitTests.Commands
{
    public class NameCommand : ICommand
    {
        public string Name { get; set; }
    }
}