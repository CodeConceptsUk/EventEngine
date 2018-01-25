using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Interfaces.Commands;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Services;
using EventEngine.Services;
using Xunit;

namespace EventEngine.UnitTests.Services
{
    public class CommandHandlerRegistryUnitTests
    {
        public CommandHandlerRegistryUnitTests()
        {
            _commandHandlerList = new ICommandHandler[] {new CommandACommandHandler(),
                new CommandACommandHandler2(), new CommandBCommandHandler()};
            _target = new CommandHandlerRegistry();
            _target.Register(_commandHandlerList);
        }

        private ICommandHandlerRegistry _target;
        private ICommandHandler[] _commandHandlerList;

        public class CommandA : ICommand
        {
        }

        public class CommandB : ICommand
        {
        }

        public class CommandC : ICommand
        {
        }

        public class CommandACommandHandler : ICommandHandler<CommandA>
        {
            public IEnumerable<IEvent> Execute(CommandA command)
            {
                throw new NotImplementedException();
            }
        }

        public class CommandACommandHandler2 : ICommandHandler<CommandA>
        {
            public IEnumerable<IEvent> Execute(CommandA command)
            {
                throw new NotImplementedException();
            }
        }

        public class CommandBCommandHandler : ICommandHandler<CommandB>
        {
            public IEnumerable<IEvent> Execute(CommandB command)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void WhenThereAreMultipleHandlersForACommandTheyAreAllReturned()
        {
            var handlers = _target.Filter(typeof(CommandA));

            Assert.Collection(handlers, 
                e => Assert.IsType<CommandACommandHandler>(e), 
                e => Assert.IsType<CommandACommandHandler2>(e));
        }

        [Fact]
        public void WhenThereAreNoHandlersForACommandAnEmptyArrayIsReturned()
        {
            var handlers = _target.Filter(typeof(CommandC));

            Assert.Empty(handlers);
        }

        [Fact]
        public void WhenThereIsJustOneHandlerForACommandItIsReturned()
        {
            var handlers = _target.Filter(typeof(CommandB));

            Assert.Collection(handlers, 
                e => Assert.IsType<CommandBCommandHandler>(e));
        }
    }
}