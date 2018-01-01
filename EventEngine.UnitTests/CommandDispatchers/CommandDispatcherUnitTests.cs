using System.Collections.Generic;
using System.Linq;
using EventEngine.Application.Dispatchers;
using EventEngine.Application.Exceptions;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Repositories;
using EventEngine.Application.Interfaces.Services;
using NSubstitute;
using NUnit.Framework;

namespace EventEngine.UnitTests.CommandDispatchers
{
    [TestFixture]
    public class CommandDispatcherUnitTests
    {        
        private IEventStore _repository;
        private ICommandDispatcher _target;
        private ICommandHandlerRegistry _commandHandlerRegistry;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IEventStore>();
            _commandHandlerRegistry = Substitute.For<ICommandHandlerRegistry>();
            _target = new CommandDispatcher(_repository, _commandHandlerRegistry);
        }
        
        private bool ValidateEventList(List<IEvent> expectedEvents, IEvent[] events)
        {
            var i = 0;
            return expectedEvents.Count == events.Length && expectedEvents.All(ev => events[i++] == ev);
        }

        public class TestCommand : ICommand
        {
        }

        [Test]
        public void WhenIExecuteACommandItIsDispatchedToItsHandler()
        {
            var commandHandler = Substitute.For<ICommandHandler<TestCommand>>();
            var actualCommandHandlerList = new ICommandHandler[] {commandHandler};
            var expectedEvents = new List<IEvent> {Substitute.For<IEvent>(), Substitute.For<IEvent>()};

            _commandHandlerRegistry.Filter(typeof(TestCommand)).Returns(actualCommandHandlerList);

            var command = new TestCommand();

            commandHandler.Execute(command).Returns(expectedEvents);

            _target.Dispatch(command);

            _repository.Received().Add(Arg.Is<IEnumerable<IEvent>>(e => ValidateEventList(expectedEvents, e.ToArray())));
        }

        [Test]
        public void WhenIExecuteACommandWithMultipleHandlersItIsDispatchedToItsHandlers()
        {
            var commandHandler1 = Substitute.For<ICommandHandler<TestCommand>>();
            var commandHandler2 = Substitute.For<ICommandHandler<TestCommand>>();
             var actualCommandHandlerList = new ICommandHandler[] {commandHandler1, commandHandler2};
            var expectedEvents = new List<IEvent> {Substitute.For<IEvent>(), Substitute.For<IEvent>()};
         
            _commandHandlerRegistry.Filter(typeof(TestCommand)).Returns(actualCommandHandlerList);

            var command = new TestCommand();

            commandHandler1.Execute(command).Returns(new[] {expectedEvents[0]});
            commandHandler2.Execute(command).Returns(new[] {expectedEvents[1]});

            _target.Dispatch(command);

            _repository.Received().Add(Arg.Is<IEnumerable<IEvent>>(e => ValidateEventList(expectedEvents, e.ToArray())));
        }

        [Test]
        public void WhenIExecuteACommandWithNoHandlerAnExceptionIsThrown()
        {
            var expectedCommand = new TestCommand();

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