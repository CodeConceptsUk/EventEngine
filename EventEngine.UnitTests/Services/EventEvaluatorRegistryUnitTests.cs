using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Interfaces;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Services;
using EventEngine.Services;
using NSubstitute;
using Xunit;

namespace EventEngine.UnitTests.Services
{
    public class EventEvaluatorRegistryUnitTests
    {
        private IEventEvaluatorRegistry _target;
        private IEventEvaluatorAttributeService _eventEvaluatorAttributeService;

        public EventEvaluatorRegistryUnitTests()
        {
            _eventEvaluatorAttributeService = Substitute.For<IEventEvaluatorAttributeService>();
            _target = new EventEvaluatorRegistry(_eventEvaluatorAttributeService);
            _eventEvaluatorAttributeService.Get(Arg.Any<Type>())
                .Returns((Guid.NewGuid().ToString(), new Version(0, 0), (Version)null));
        }

        private IEventEvaluator CreateEventEvaluatorSubstitute(bool interfaceMatches, bool nameMatches,
            string minimumVersionCondition, string maximumVersionCondition, string expectedEventName, Version expectedVersion)
        {
            var eventEvaluator = interfaceMatches ? (IEventEvaluator)Substitute.For<IEventEvaluator<TestView, IEventData>>() : Substitute.For<IEventEvaluator<SomeOtherView, IEventData>>();
            Version minimumVersion = null;
            Version maximumVersion = null;
            var eventName = nameMatches ? expectedEventName : Guid.NewGuid().ToString();

            switch (minimumVersionCondition) // is {} than event version
            {
                case "LT":
                    minimumVersion = new Version(expectedVersion.Major, expectedVersion.Minor, expectedVersion.Build, expectedVersion.Revision - 1);
                    break;
                case "EQ":
                    minimumVersion = expectedVersion;
                    break;
                case "GT":
                    minimumVersion = new Version(expectedVersion.Major, expectedVersion.Minor, expectedVersion.Build, expectedVersion.Revision + 1);
                    break;
            }
            switch (maximumVersionCondition) // is {} than event version
            {
                case "LT":
                    maximumVersion = new Version(expectedVersion.Major, expectedVersion.Minor, expectedVersion.Build, expectedVersion.Revision - 1);
                    break;
                case "EQ":
                    maximumVersion = expectedVersion;
                    break;
                case "GT":
                    maximumVersion = new Version(expectedVersion.Major, expectedVersion.Minor, expectedVersion.Build, expectedVersion.Revision + 1);
                    break;
                case null:
                    maximumVersion = (Version)null;
                    break;
            }

            _eventEvaluatorAttributeService.Get(eventEvaluator.GetType()).Returns((eventName, minimumVersion, maximumVersion));
            return eventEvaluator;
        }

        public class TestView : IView
        {
        }

        public class SomeOtherView : IView
        {
        }

        [Theory, CombinatorialData]
        public void WhenIFilterEventEvaluatorsTheyAreFilteredCorrectly(
            [CombinatorialValues(1, 2, 3)] int matchingQuantity,
            [CombinatorialValues(true, false)] bool interfaceMatches,
            [CombinatorialValues(true, false)] bool nameMatches,
            [CombinatorialValues("LT", "EQ", "GT")] string minimumVersionCondition,
            [CombinatorialValues("LT", "EQ", "GT", null)] string maximumVersionCondition)
        {
            var expectedEventName = Guid.NewGuid().ToString();
            var expectedVersion = new Version(1, 2, 3, 4);

            var eventType = Substitute.For<IEventType>();
            eventType.Name.Returns(expectedEventName);
            eventType.Version.Returns(expectedVersion);

            var shouldMatch = interfaceMatches
                              && nameMatches
                              && (minimumVersionCondition == "EQ" || minimumVersionCondition == "LT") // minimum version is {} than event version
                              && (maximumVersionCondition == null || maximumVersionCondition == "GT" || maximumVersionCondition == "EQ"); // maximum version is {} than event version

            Console.WriteLine(shouldMatch ? "Expecting to match" : "Expecting no matches");

            var expectedEvaluators = new List<IEventEvaluator>();

            for (var i = 1; i <= matchingQuantity; i++)
            {
                var expectedEvaluator = CreateEventEvaluatorSubstitute(interfaceMatches, nameMatches, minimumVersionCondition,
                    maximumVersionCondition, expectedEventName, expectedVersion);
                expectedEvaluators.Add(expectedEvaluator);
                _target.Register(expectedEvaluator);
            }

            _target.Register(Substitute.For<IEventEvaluator<SomeOtherView, IEventData>>());

            var matchingEvaluators = _target.Filter<TestView>(eventType);

            if (!shouldMatch)
            {
                Assert.Empty(matchingEvaluators);
            }
            else
            {
                var actions = expectedEvaluators.Select<IEventEvaluator, Action<IEventEvaluator>>(q => (e => Assert.Same(q, e))).ToArray();
                Assert.Collection(matchingEvaluators, actions);
            }
        }
    }
}