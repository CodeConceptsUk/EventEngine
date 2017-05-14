using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeConcepts.CliConsole.Interfaces;
using CodeConcepts.CliConsole.Interfaces.Convertors;

namespace CodeConcepts.CliConsole
{
    public class ConsoleParser : IConsoleParser
    {
        private readonly IValueConvertor _valueConvertor;
        private readonly IConsoleProxy _console;

        public ConsoleParser(IValueConvertor valueConvertor, IConsoleProxy console)
        {
            _valueConvertor = valueConvertor;
            _console = console;
        }

        public bool Parse(ICliCommand cliCommand, string[] args)
        {
            var argsList = args.ToList();
            var arguments = cliCommand.Arguments;

            foreach (var argument in arguments)
            {
                var inputValue = GetArgumentValue(argsList, argument);

                do
                {
                    if (RequiresConsoleInput(inputValue))
                    {
                        if (!argument.IsRequired)
                            break;

                        inputValue = GetInputFromConsole(argument, _console);
                        if (ExitCommandRecieved(inputValue))
                        {
                            _console.WriteLine($"Exiting command {cliCommand.CommandName}");
                            return false;
                        }
                    }

                    if (ExecuteCommandArgumentAction(argument, inputValue))
                        continue;

                    inputValue = null;
                    _console.Write($"{TabIndent}Invalid input for {argument.Name}");

                } while (!InputValueAccepted(inputValue));
            }

            return true;
        }

        private static bool RequiresConsoleInput(string inputValue)
        {
            return string.IsNullOrEmpty(inputValue);
        }

        private static bool InputValueAccepted(string argValue)
        {
            return argValue != null;
        }

        private static bool ExitCommandRecieved(string argValue)
        {
            return string.Equals(argValue, null);
        }

        private bool ExecuteCommandArgumentAction(CommandArgument argument, string argValue)
        {

            var convertType = argument.GetType().GetGenericArguments()[0];
            if (!_valueConvertor.TryConvert(convertType, argValue, out object value))
                return false;

            InvokeArgumentAction(argument, value);
            return true;
        }

        private static void InvokeArgumentAction(CommandArgument argument, object value)
        {
            var propertyInfo = argument.GetType().GetProperty("Action", BindingFlags.Public | BindingFlags.Instance);
            var delegateValue = propertyInfo?.GetValue(argument);
            var invokeMethod = delegateValue?.GetType()
                .GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
            invokeMethod?.Invoke(delegateValue, new[] {value});
        }

        private static string GetArgumentValue(IList<string> args, CommandArgument argument)
        {
            var arg = args.SingleOrDefault(a => IsArgCommand(argument, a));
            var index = args.IndexOf(arg);

            if (arg == null)
                return null;

            return (index + 1) > args.Count
                ? null
                : args[index + 1];
        }

        private static string GetInputFromConsole(CommandArgument argument, IConsoleProxy console)
        {
            console.Write($"{TabIndent}{argument.Name} ({argument.Description}): ");
            return console.ReadLine();
        }

        private static bool IsArgCommand(CommandArgument argument, string arg)
        {
            return string.Equals(arg, $"-{argument.Name}", StringComparison.InvariantCultureIgnoreCase) ||
                   string.Equals(arg, $"/{argument.Name}", StringComparison.InvariantCultureIgnoreCase) ||
                   string.Equals(arg, $"{argument.Name}", StringComparison.InvariantCultureIgnoreCase);

        }

        private const string TabIndent = "    ";
    }
}