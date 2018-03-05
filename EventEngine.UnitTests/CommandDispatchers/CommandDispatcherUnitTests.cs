using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Dispatchers;
using EventEngine.Exceptions;
using EventEngine.Interfaces.Commands;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Repositories;
using EventEngine.Interfaces.Services;
using NSubstitute;
using Xunit;

namespace EventEngine.UnitTests.CommandDispatchers
{
    public class CommandDispatcherUnitTests
    {
        private readonly IEventStore _repository;
        private readonly ICommandDispatcher _target;
        private readonly ICommandHandlerRegistry _commandHandlerRegistry;

        public CommandDispatcherUnitTests()
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

        [Fact]
        public void WhenIExecuteACommandItIsDispatchedToItsHandler()
        {
            var commandHandler = Substitute.For<ICommandHandler<TestCommand>>();
            var actualCommandHandlerList = new ICommandHandler[] { commandHandler };
            var expectedEvents = new List<IEvent> { Substitute.For<IEvent>(), Substitute.For<IEvent>() };
            var expectedContextId = Guid.NewGuid();

            _commandHandlerRegistry.Filter(typeof(TestCommand)).Returns(actualCommandHandlerList);

            var command = new TestCommand();

            commandHandler.Execute(expectedContextId, command).Returns(expectedEvents);

            _target.Dispatch(expectedContextId, command);

            _repository.Received().Add(Arg.Is<IEnumerable<IEvent>>(e => ValidateEventList(expectedEvents, e.ToArray())));
        }

        [Fact]
        public void WhenIExecuteACommandWithMultipleHandlersItIsDispatchedToItsHandlers()
        {
            var commandHandler1 = Substitute.For<ICommandHandler<TestCommand>>();
            var commandHandler2 = Substitute.For<ICommandHandler<TestCommand>>();
            var actualCommandHandlerList = new ICommandHandler[] { commandHandler1, commandHandler2 };
            var expectedEvents = new List<IEvent> { Substitute.For<IEvent>(), Substitute.For<IEvent>() };
            var expectedContextId = Guid.NewGuid();

            _commandHandlerRegistry.Filter(typeof(TestCommand)).Returns(actualCommandHandlerList);

            var command = new TestCommand();

            commandHandler1.Execute(expectedContextId, command).Returns(new[] { expectedEvents[0] });
            commandHandler2.Execute(expectedContextId, command).Returns(new[] { expectedEvents[1] });

            _target.Dispatch(expectedContextId, command);

            _repository.Received().Add(Arg.Is<IEnumerable<IEvent>>(e => ValidateEventList(expectedEvents, e.ToArray())));
        }

        [Fact]
        public void WhenIExecuteACommandWithNoHandlerAnExceptionIsThrown()
        {
            var expectedCommand = new TestCommand();

            try
            {
                _target.Dispatch(Guid.NewGuid(), expectedCommand);
                Assert.True(false, "Expected expcetion to be thrown");
            }
            catch (EventEngineMissingCommandHandlerException exception)
            {
                Assert.Same(expectedCommand, exception.Command);
            }
        }
    }
}