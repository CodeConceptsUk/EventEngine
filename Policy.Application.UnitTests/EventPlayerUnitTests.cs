using Microsoft.Practices.Unity;
using NSubstitute;
using NUnit.Framework;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Factories;

namespace Policy.Application.UnitTests
{
    [TestFixture]
    public class EventPlayerUnitTests
    {
        private IEventPlayer<IEvent> _target;
        private IUnityContainer _container;
        private ILogFactory _logFactory;

        [SetUp]
        public void Setup()
        {
            _logFactory = Substitute.For<ILogFactory>();
            _container = Substitute.For<IUnityContainer>();
            _target = new EventPlayer<IEvent>(_container, _logFactory);
        }

        [Test]
        public void Test1()
        {
            Assert.Inconclusive("Not Yet Written");
        }
    }
}