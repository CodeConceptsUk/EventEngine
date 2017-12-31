//using System;
//using EventEngine.Application.Interfaces.Events;
//using EventEngine.Application.Interfaces.Factories;
//using EventEngine.Application.Interfaces.Repositories;
//using EventEngine.Application.PropertyBags;
//using NSubstitute;
//using NUnit.Framework;

//namespace EventEngine.UnitTests.PropertyBags
//{
//    [TestFixture]
//    public class EventUnitTests
//    {
//        private IEventFactory _target;

//        [SetUp]
//        public void SetUp()
//        {
//            _target = new 
//        }

//        [Test]
//        public void WhenICreateAnEventItIsCreated()
//        {
//            var expectedContextId = Guid.NewGuid();
//            var expectedEventData = Guid.NewGuid().ToString();
//            var expectedEventType = Substitute.For<IEventType>();
//            var expectedEventDateTime = DateTime.Now;

//            var target = new Event(expectedContextId, expectedEventType, expectedEventData, expectedEventDateTime);

//            Assert.AreEqual(expectedContextId, target.ContextId);
//            Assert.AreEqual(expectedEventDateTime, target.EventDateTime);
//            Assert.AreSame(expectedEventType, target.EventType);
//            Assert.AreEqual(expectedEventData, target.EventData);
//        }
//    }
//}