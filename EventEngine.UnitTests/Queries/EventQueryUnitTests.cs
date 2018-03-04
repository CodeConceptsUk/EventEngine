using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using EventEngine.Interfaces;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Repositories;
using EventEngine.Interfaces.Services;
using EventEngine.Players;
using EventEngine.Queries;
using NSubstitute;
using Xunit;

namespace EventEngine.UnitTests.Queries
{
    public class EventQueryUnitTests
    {
        [Theory, AutoNSubstituteData]
        public void WhenIGetTheQueryWithOnlyTheContextId(IEventStore eventStore, IEventPlayer eventPlayer,
            IUndoEventProcessingService undoEventProcessingService)
        {
            TestView expectedView = null;
            var events = Substitute.For<IList<IEvent>>();
            var eventsToProcess = Substitute.For<IList<IEvent>>();
            var contextId = Guid.NewGuid();
            var target = new EventQuery<TestView>(eventStore, eventPlayer, undoEventProcessingService);

            eventStore.Get(contextId).Returns(events);
            undoEventProcessingService.Execute(events).Returns(eventsToProcess);

            var result = target.Get(contextId);

            eventPlayer.Received().Play(eventsToProcess, Arg.Is<TestView>(a => GetView(a, out expectedView)));
            Assert.Same(expectedView, result);
        }

        [Theory, AutoNSubstituteData]
        public void WhenIGetTheQueryWithTheContextIdAndTo(IEventStore eventStore, IEventPlayer eventPlayer,
               IUndoEventProcessingService undoEventProcessingService)
        {
            TestView expectedView = null;
            var fixture = new Fixture().Customize(new AutoConfiguredNSubstituteCustomization());
            var day = 0;
            fixture.Register(() =>
            {
                day++;
                return new DateTime().AddDays(day);
            }); 
            var events = fixture.CreateMany<IEvent>(10).OrderBy(t => t.CreatedDateTime);
            var eventsToProcess = Substitute.For<IList<IEvent>>();
            var contextId = Guid.NewGuid();
            var target = new EventQuery<TestView>(eventStore, eventPlayer, undoEventProcessingService);

            eventStore.Get(contextId).Returns(events);
            undoEventProcessingService.Execute(Arg.Is<IEnumerable<IEvent>>(t => t.Count() == 5)).Returns(eventsToProcess);

            var result = target.Get(contextId, events.ElementAt(4).CreatedDateTime);

            eventPlayer.Received().Play(eventsToProcess, Arg.Is<TestView>(a => GetView(a, out expectedView)));
            Assert.Same(expectedView, result);
        }

        private static bool GetView(TestView testView, out TestView expectedView)
        {
            expectedView = testView;
            return true;
        }

        public class TestView : IView
        {
        }
    }
}