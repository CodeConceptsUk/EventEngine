namespace CliConsole.Interfaces
{
    public interface IConsoleProxy
    {
        string ReadLine();
        void Write(string value);
        void WriteLine(string value);
    }
}