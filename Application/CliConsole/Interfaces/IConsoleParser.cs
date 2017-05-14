namespace CodeConcepts.CliConsole.Interfaces
{
    public interface IConsoleParser
    {
        bool Parse(ICliCommand cliCommand, string[] args);
    }
}