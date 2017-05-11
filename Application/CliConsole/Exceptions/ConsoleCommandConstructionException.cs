namespace CliConsole.Exceptions
{
    public class ConsoleCommandConstructionException : ConsoleException
    {
        public override string Message => $"Console command could not be constructed no command " +
                                          $"name was provided using IsCommand(<name>, <description>)";
    }
}   