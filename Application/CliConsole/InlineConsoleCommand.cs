﻿using System;
using System.Collections.Generic;
using CliConsole.Exceptions;
using CliConsole.Interfaces;

namespace CliConsole
{
    public abstract class InlineConsoleCommand : ICommand
    {
        protected InlineConsoleCommand(string commandName, string description)
        {
            if (string.IsNullOrWhiteSpace(commandName))
                throw new ConsoleCommandConstructionException();

            CommandName = commandName;
            Description = description;
        }

        private readonly IList<CommandArgument> _commandArguments = new List<CommandArgument>();

        public IEnumerable<CommandArgument> Arguments => _commandArguments;

        public void Run()
        {
            Execute();
        }
        
        protected void HasRequiredOption<TType>(string name, string description, Action<TType> action)
        {
            _commandArguments.Add(new CommandArgument<TType>(name, description, action, true));
        }

        protected void HasOption<TType>(string name, string description, Action<TType> action)
        {
            _commandArguments.Add(new CommandArgument<TType>(name, description, action));
        }

        public string CommandName { get; private set; }

        public string Description { get; private set; }

        protected abstract void Execute();
    }
}