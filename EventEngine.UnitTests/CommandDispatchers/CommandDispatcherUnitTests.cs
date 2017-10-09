using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Application.Exceptions;
using EventEngine.Application.Factories;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.Application.Interfaces.Repositories;
using EventEngine.UnitTests.CommandHandlers;
using EventEngine.UnitTests.Commands;
using EventEngine.UnitTests.Events;
using NSubstitute;
using NUnit.Framework;

namespace EventEngine.UnitTests.CommandDispatchers
{
    [TestFixture]
    public class CommandDispatcherUnitTests
    {
        private ICommandDispatcherFactory _factory;
        private IEventStore _repository;
        private ICommandDispatcher _target;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IEventStore>();
            _factory = new CommandDispatcherFactory();
        }

        [Test]
        public void WhenIExecuteACommandItIsDispatchedToItsHandler()
        {
            _target = _factory.Create(_repository, new NameCommandHandler());

            var expectedName = Guid.NewGuid().ToString();

            _target.Dispatch(new NameCommand { Name = expectedName });

            _repository.Received().Add(Arg.Is<IEnumerable<IEvent>>(events => events.OfType<NameEvent>().Count(@event => @event.Name.Equals(expectedName)) == 1));
        }

        [Test]
        public void WhenIExecuteACommandWithMultipleHandlersItIsDispatchedToItsHandlers()
        {
            _target = _factory.Create(_repository, new NameCommandHandler(), new NameCommandHandler());

            var expectedName = Guid.NewGuid().ToString();

            _target.Dispatch(new NameCommand { Name = expectedName });

            _repository.Received(1).Add(Arg.Is<IEnumerable<IEvent>>(events => events.OfType<NameEvent>().Count(@event => @event.Name.Equals(expectedName)) == 2));
        }

        [Test]
        public void WhenIExecuteACommandWithNoHandlerAnExceptionIsThrown()
        {
            _target = _factory.Create(_repository);
            var expectedCommand = new NoHandlerCommand();

            try
            {
                _target.Dispatch(expectedCommand);
                Assert.Fail("Expected expcetion to be thrown");
            }
            catch (EventEngineMissingCommandHandlerException exception)
            {
                Assert.AreSame(expectedCommand, exception.Command);
            }
        }
    }
}