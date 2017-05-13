namespace CodeConcepts.CliConsole.Interfaces
{
    public interface IConsoleProxy
    {
        string ReadLine();
        void Write(params object[] value);
        void WriteLine(params object[] value);
    }
}