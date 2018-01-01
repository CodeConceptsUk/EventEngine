using System.Collections.Generic;
using System.Linq;
using EventEngine.Application.Exceptions;
using EventEngine.Application.Factories;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.Application.Interfaces.Repositories;
using EventEngine.Application.Interfaces.Services;
using NSubstitute;
using NUnit.Framework;

namespace EventEngine.UnitTests.CommandDispatchers
{
    [TestFixture]
    public class CommandDispatcherUnitTests
    {
        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IEventStore>();
            _commandHandlerFilteringService = Substitute.For<ICommandHandlerFilteringService>();
            _factory = new CommandDispatcherFactory(_repository, _commandHandlerFilteringService);
        }

        private ICommandDispatcherFactory _factory;
        private IEventStore _repository;
        private ICommandDispatcher _target;
        private ICommandHandlerFilteringService _commandHandlerFilteringService;

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
            var inputCommandHandlerList = new ICommandHandler[] {Substitute.For<ICommandHandler<TestCommand>>()};
            var actualCommandHandlerList = new ICommandHandler[] {commandHandler};
            var expectedEvents = new List<IEvent> {Substitute.For<IEvent>(), Substitute.For<IEvent>()};
            _target = _factory.Create(inputCommandHandlerList);

            _commandHandlerFilteringService.Filter(inputCommandHandlerList, typeof(TestCommand)).Returns(actualCommandHandlerList);

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
            var inputCommandHandlerList = new ICommandHandler[] {Substitute.For<ICommandHandler<TestCommand>>()};
            var actualCommandHandlerList = new ICommandHandler[] {commandHandler1, commandHandler2};
            var expectedEvents = new List<IEvent> {Substitute.For<IEvent>(), Substitute.For<IEvent>()};
            _target = _factory.Create(inputCommandHandlerList);

            _commandHandlerFilteringService.Filter(inputCommandHandlerList, typeof(TestCommand)).Returns(actualCommandHandlerList);

            var command = new TestCommand();

            commandHandler1.Execute(command).Returns(new[] {expectedEvents[0]});
            commandHandler2.Execute(command).Returns(new[] {expectedEvents[1]});

            _target.Dispatch(command);

            _repository.Received().Add(Arg.Is<IEnumerable<IEvent>>(e => ValidateEventList(expectedEvents, e.ToArray())));
        }

        [Test]
        public void WhenIExecuteACommandWithNoHandlerAnExceptionIsThrown()
        {
            _target = _factory.Create(new ICommandHandler[] { });
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