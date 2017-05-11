namespace CodeConcepts.CliConsole.Interfaces
{
    public interface IConsoleParser
    {
        bool Parse(ICommand command, string[] args);
    }
}