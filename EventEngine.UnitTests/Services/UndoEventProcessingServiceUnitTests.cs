using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using EventEngine.Events;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Services;
using EventEngine.Services;
using NSubstitute;
using Xunit;

namespace EventEngine.UnitTests.Services
{
    public class UndoEventProcessingServiceUnitTests
    {
        [Theory, AutoNSubstituteData]
        public void WhenIUndoEventsTheyAreUndone(IEventDataDeserializationService eventDataDeserializationService)
        {
            var target = new UndoEventProcessingService(eventDataDeserializationService);
            var eventDataSerialized = Guid.NewGuid().ToString();
            var fixture = new Fixture().Customize(new AutoConfiguredNSubstituteCustomization());
            fixture.Register(() => false);
            var events = fixture.CreateMany<IEvent>(4).OrderBy(t => t.EffectiveDateTime).ToList();
            var undoEvent = Substitute.For<IEvent>();
            var eventData = new UndoData(events.Select(t => t.EventId).ToList());
            var latestEvent = events.Max(t => t.EffectiveDateTime).AddSeconds(1);
            var expectedToBeUndone = new List<IEvent>();
            expectedToBeUndone.AddRange(events);

            undoEvent.EventType.Name.Returns("Undo");
            undoEvent.EventData.Returns(eventDataSerialized);
            undoEvent.EffectiveDateTime.Returns(latestEvent);
            events.Add(undoEvent);

            eventDataDeserializationService.Deserialize(typeof(UndoData), eventDataSerialized).Returns(eventData);
            target.Execute(events);

            Assert.All(expectedToBeUndone, @event => @event.Received().Undone = true);
        }
    }
}