using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Application.Interfaces;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;
using EventEngine.Application.Services;
using NSubstitute;
using NUnit.Framework;

namespace EventEngine.UnitTests.Services
{
    [TestFixture]
    public class EventEvaluatorFilteringServiceUnitTests
    {
        [SetUp]
        public void SetUp()
        {
            _target = new EventEvaluatorFilteringService();
        }

        private IEventEvaluatorFilteringService _target;

        private static IEventEvaluator CreateEventEvaluatorSubstitute(bool interfaceMatches, bool nameMatches, string minimumVersion, string maximumVersion, string expectedEventName, Version expectedVersion)
        {
            var eventEvaluator = interfaceMatches ? (IEventEvaluator) Substitute.For<IEventEvaluator<TestView>>() : Substitute.For<IEventEvaluator<SomeOtherView>>();
            eventEvaluator.Name.Returns(nameMatches ? expectedEventName : Guid.NewGuid().ToString());
            switch (minimumVersion) // is {} than event version
            {
                case "LT":
                    eventEvaluator.MinimumVersion.Returns(new Version(expectedVersion.Major, expectedVersion.Minor, expectedVersion.Build, expectedVersion.Revision - 1));
                    break;
                case "EQ":
                    eventEvaluator.MinimumVersion.Returns(expectedVersion);
                    break;
                case "GT":
                    eventEvaluator.MinimumVersion.Returns(new Version(expectedVersion.Major, expectedVersion.Minor, expectedVersion.Build, expectedVersion.Revision + 1));
                    break;
            }
            switch (maximumVersion) // is {} than event version
            {
                case "LT":
                    eventEvaluator.MaximumVersion.Returns(new Version(expectedVersion.Major, expectedVersion.Minor, expectedVersion.Build, expectedVersion.Revision - 1));
                    break;
                case "EQ":
                    eventEvaluator.MaximumVersion.Returns(expectedVersion);
                    break;
                case "GT":
                    eventEvaluator.MaximumVersion.Returns(new Version(expectedVersion.Major, expectedVersion.Minor, expectedVersion.Build, expectedVersion.Revision + 1));
                    break;
                case null:
                    eventEvaluator.MaximumVersion.Returns((Version) null);
                    break;
            }
            return eventEvaluator;
        }

        public class TestView : IView
        {
        }

        public class SomeOtherView : IView
        {
        }

        [Test]
        public void WhenIFilterEventEvaluatorsTheyAreFilteredCorrectly(
            [Values(1, 2, 3)] int matchingQuantity,
            [Values(true, false)] bool interfaceMatches,
            [Values(true, false)] bool nameMatches,
            [Values("LT", "EQ", "GT")] string minimumVersion,
            [Values("LT", "EQ", "GT", null)] string maximumVersion)
        {
            var evaluators = new List<IEventEvaluator>();

            var expectedEventName = Guid.NewGuid().ToString();
            var expectedVersion = new Version(1, 2, 3, 4);

            var eventType = Substitute.For<IEventType>();
            eventType.Type.Returns(expectedEventName);
            eventType.Version.Returns(expectedVersion);

            var shouldMatch = interfaceMatches
                              && nameMatches
                              && (minimumVersion == "EQ" || minimumVersion == "LT") // minimum version is {} than event version
                              && (maximumVersion == null || maximumVersion == "GT" || maximumVersion == "EQ"); // maximum version is {} than event version

            var expectedEvaluators = new List<IEventEvaluator>();

            for (var i = 1; i <= matchingQuantity; i++)
                expectedEvaluators.Add(CreateEventEvaluatorSubstitute(interfaceMatches, nameMatches, minimumVersion, maximumVersion, expectedEventName, expectedVersion));

            evaluators.AddRange(expectedEvaluators);
            evaluators.Add(Substitute.For<IEventEvaluator>());
            evaluators.Add(Substitute.For<IEventEvaluator>());
            evaluators.Add(Substitute.For<IEventEvaluator>());
            evaluators.Add(Substitute.For<IEventEvaluator>());

            var matchingEvaluators = _target.Filter<TestView>(evaluators.ToArray(), eventType);

            if (!shouldMatch)
            {
                Assert.IsTrue(!matchingEvaluators.Any());
            }
            else
            {
                Assert.AreEqual(matchingQuantity, matchingEvaluators.Length);
                foreach (var eventEvaluator in expectedEvaluators)
                    Assert.IsTrue(matchingEvaluators.Contains(eventEvaluator));
            }
        }
    }
}