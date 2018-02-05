using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Interfaces.Commands;
using EventEngine.Interfaces.Services;

namespace EventEngine.Services
{
    public class CommandHandlerRegistry : ICommandHandlerRegistry
    {
        private class CommandHandlerListItem {
            public Type CommandType {get;set;}
            public ICommandHandler CommandHandler {get;set;}
            public CommandHandlerListItem(Type commandType, ICommandHandler commandHandler)
            {
                CommandType = commandType;
                CommandHandler = commandHandler;
            }
        }
    
        private readonly List<CommandHandlerListItem> _commandHandlers;

        public CommandHandlerRegistry()
        {
            _commandHandlers = new List<CommandHandlerListItem>();
        }

        public void Register(params ICommandHandler[] commandHandlers)
        {
            foreach (var commandHandler in commandHandlers)
            {
                var commandTypes = commandHandler
                    .GetType()
                    .GetInterfaces()
                    .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                    .Select(t => t.GetGenericArguments()[0]).ToArray();

                foreach (var commandType in commandTypes)
                {
                    _commandHandlers.Add(new CommandHandlerListItem(commandType, commandHandler));
                }
            }
        }

        public ICommandHandler[] Filter(Type commandType)
        {
            return _commandHandlers
                .Where(t => t.CommandType == commandType)
                .Select(t => t.CommandHandler).ToArray();
        }
    }
}
