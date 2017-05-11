using System;
using System.Collections.Generic;
using CodeConcepts.FrameworkExtensions.LinqExtensions;
using NUnit.Framework;

namespace CodeConcepts.FrameworkExtensions.UnitTests
{
    [TestFixture]
    public class ForEachExtensionsUnitTests
    {
        private Guid[] _collection;

        [SetUp]
        public void Setup()
        {
            _collection = new[]
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
            };
        }

        [Test]
        public void WhenILoopUsingForEachThenTheActionIsExecutedForEveryMemberOfTheCollection()
        {
            var results = new List<Guid>();

            _collection.ForEach(results.Add);

            Assert.AreEqual(_collection.Length, results.Count);
            for (var i = 0; i < _collection.Length; i++)
            {
                Assert.AreEqual(_collection[i], results[i]);
            }
        }
    }
}
