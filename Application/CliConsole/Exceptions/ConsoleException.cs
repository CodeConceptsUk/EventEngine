using System;

namespace CodeConcepts.CliConsole.Exceptions
{
    public class ConsoleException : Exception
    {
        public ConsoleException()
        {
        }

        public ConsoleException(string message)
            : base(message)
        {
        }
    }
}