using System;
using CodeConcepts.CliConsole.Interfaces;
using CodeConcepts.FrameworkExtensions.LinqExtensions;

namespace CodeConcepts.CliConsole
{
    public class ConsoleProxy : IConsoleProxy
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(params object[] value)
        {
            value.ForEach(c =>
            {
                Console.Write(c);
            });
            Console.Write(Environment.NewLine);
        }

        public void Write(params object[] value)
        {
            value.ForEach(c =>
            {
                Console.Write(c);
            });
        }
    }
}