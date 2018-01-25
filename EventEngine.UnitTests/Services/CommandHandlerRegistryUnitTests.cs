using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Interfaces.Commands;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Services;
using EventEngine.Services;
using NUnit.Framework;

namespace EventEngine.UnitTests.Services
{
    [TestFixture]
    public class CommandHandlerRegistryUnitTests
    {
        [SetUp]
        public void SetUp()
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

        [Test]
        public void WhenThereAreMultipleHandlersForACommandTheyAreAllReturned()
        {
            var handlers = _target.Filter(typeof(CommandA));

            Assert.AreEqual(2, handlers.Length);
            Assert.IsNotNull(handlers.SingleOrDefault(h => h.GetType() == typeof(CommandACommandHandler)));
            Assert.IsNotNull(handlers.SingleOrDefault(h => h.GetType() == typeof(CommandACommandHandler2)));
        }

        [Test]
        public void WhenThereAreNoHandlersForACommandAnEmptyArrayIsReturned()
        {
            var handlers = _target.Filter(typeof(CommandC));

            Assert.AreEqual(0, handlers.Length);
        }

        [Test]
        public void WhenThereIsJustOneHandlerForACommandItIsReturned()
        {
            var handlers = _target.Filter(typeof(CommandB));

            Assert.AreEqual(1, handlers.Length);
            Assert.IsNotNull(handlers.SingleOrDefault(h => h.GetType() == typeof(CommandBCommandHandler)));
        }
    }
}