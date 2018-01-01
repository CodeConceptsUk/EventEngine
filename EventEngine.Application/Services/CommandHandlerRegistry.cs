using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Services;

namespace EventEngine.Application.Services
{
    public class CommandHandlerRegistry : ICommandHandlerRegistry
    {
        private readonly List<(Type CommandType, ICommandHandler CommandHandler)> _commandHandlers;

        public CommandHandlerRegistry()
        {
            _commandHandlers = new List<(Type CommandType, ICommandHandler CommandHandler)>();
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
                    _commandHandlers.Add((commandType, commandHandler));
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