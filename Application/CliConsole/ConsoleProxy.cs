using System;
using CliConsole.Interfaces;

namespace CliConsole
{
    public class ConsoleProxy : IConsoleProxy
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void Write(string value)
        {
            Console.Write(value);
        }
    }
}