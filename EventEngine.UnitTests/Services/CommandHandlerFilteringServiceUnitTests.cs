using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;
using EventEngine.Application.Services;
using NUnit.Framework;

namespace EventEngine.UnitTests.Services
{

    [TestFixture]
    public class CommandHandlerFilteringServiceUnitTests
    {

        private ICommandHandlerFilteringService _target;
        private ICommandHandler[] _commandHandlerList;

        [SetUp]
        public void SetUp()
        {
            _commandHandlerList = new ICommandHandler[] {new CommandACommandHandler(), new CommandACommandHandler2(), new CommandBCommandHandler()};
            _target = new CommandHandlerFilteringService();
        }

        [Test]
        public void WhenThereAreMultipleHandlersForACommandTheyAreAllReturned()
        {
            var handlers = _target.Filter(_commandHandlerList, typeof(CommandA));

            Assert.AreEqual(2, handlers.Length);
            Assert.IsNotNull(handlers.SingleOrDefault(h => h.GetType() == typeof(CommandACommandHandler)));
            Assert.IsNotNull(handlers.SingleOrDefault(h => h.GetType() == typeof(CommandACommandHandler2)));
        }

        [Test]
        public void WhenThereIsJustOneHandlerForACommandItIsReturned()
        {
            var handlers = _target.Filter(_commandHandlerList, typeof(CommandB));

            Assert.AreEqual(1, handlers.Length);
            Assert.IsNotNull(handlers.SingleOrDefault(h => h.GetType() == typeof(CommandBCommandHandler)));
        }

        [Test]
        public void WhenThereAreNoHandlersForACommandAnEmptyArrayIsReturned()
        {
            var handlers = _target.Filter(_commandHandlerList, typeof(CommandC));

            Assert.AreEqual(0, handlers.Length);
        }

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


    }
}