using System;
using System.Linq;
using CodeConcepts.CliConsole.Exceptions;
using NUnit.Framework;

namespace CodeConcepts.CliConsole.UnitTests
{
    [TestFixture]
    public class InlineConsoleCommandUnitTests
    {
        [Test]
        public void WhenIConstructACliCommandWithANameAsNullThenAnExceptionIsThrown()
        {
            try
            {
                var target = new TestInlineConsoleCliCommand(null, null);
                Assert.Fail("This should not execute");
            }
            catch (ConsoleCommandConstructionException)
            {
            }
        }

        [Test]
        public void WhenIConstructACliCommandWithoutANameThenAnExceptionIsThrown()
        {
            try
            {
                var target = new TestInlineConsoleCliCommand("  ", null);
                Assert.Fail("This should not execute");
            }
            catch (ConsoleCommandConstructionException)
            {
            }
        }

        [Test]
        public void WhenIConstructACliCommandWithANameThenItCanBeCreated()
        {

            var expectedCommandName = Guid.NewGuid().ToString();
            var expectedDescription = Guid.NewGuid().ToString();

            var target = new TestInlineConsoleCliCommand(expectedCommandName, expectedDescription);

            Assert.AreEqual(expectedCommandName, target.CommandName);
            Assert.AreEqual(expectedDescription, target.Description);
        }

        [Test]
        public void WhenIConstructACliCommandAndAddARequiredParametersTheyAreAdded()
        {
            var actionResult1 = 0;
            var expectedActionResult1 = 12345;
            var expectedOptionName1 = Guid.NewGuid().ToString();
            var expectedOptionDescription1 = Guid.NewGuid().ToString();

            var actionResult2 = 0;
            var expectedActionResult2 = 34543543;
            var expectedOptionName2 = Guid.NewGuid().ToString();
            var expectedOptionDescription2 = Guid.NewGuid().ToString();


            var target = new TestInlineConsoleCliCommand(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            target.HasRequiredOption<int>(expectedOptionName1, expectedOptionDescription1, p => actionResult1 = p);
            target.HasRequiredOption<int>(expectedOptionName2, expectedOptionDescription2, p => actionResult2 = p);

            var argument1 = target.Arguments.ElementAt(0);
            var argument2 = target.Arguments.ElementAt(1);
            ((CommandArgument<int>)argument1).Action(expectedActionResult1);
            ((CommandArgument<int>)argument2).Action(expectedActionResult2);

            Assert.AreEqual(expectedOptionName1, argument1.Name);
            Assert.AreEqual(expectedOptionDescription1, argument1.Description);
            Assert.AreEqual(actionResult1, expectedActionResult1);
            Assert.IsTrue(argument1.IsRequired);
            Assert.AreEqual(expectedOptionName2, argument2.Name);
            Assert.AreEqual(expectedOptionDescription2, argument2.Description);
            Assert.AreEqual(actionResult2, expectedActionResult2);
            Assert.IsTrue(argument2.IsRequired);
        }

        [Test]
        public void WhenIConstructACliCommandAndAddAParametersTheyAreAdded()
        {
            var actionResult1 = 0;
            var expectedActionResult1 = 12345;
            var expectedOptionName1 = Guid.NewGuid().ToString();
            var expectedOptionDescription1 = Guid.NewGuid().ToString();

            var actionResult2 = 0;
            var expectedActionResult2 = 34543543;
            var expectedOptionName2 = Guid.NewGuid().ToString();
            var expectedOptionDescription2 = Guid.NewGuid().ToString();


            var target = new TestInlineConsoleCliCommand(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            target.HasOption<int>(expectedOptionName1, expectedOptionDescription1, p => actionResult1 = p);
            target.HasOption<int>(expectedOptionName2, expectedOptionDescription2, p => actionResult2 = p);

            var argument1 = target.Arguments.ElementAt(0);
            var argument2 = target.Arguments.ElementAt(1);
            ((CommandArgument<int>)argument1).Action(expectedActionResult1);
            ((CommandArgument<int>)argument2).Action(expectedActionResult2);

            Assert.AreEqual(expectedOptionName1, argument1.Name);
            Assert.AreEqual(expectedOptionDescription1, argument1.Description);
            Assert.AreEqual(actionResult1, expectedActionResult1);
            Assert.IsFalse(argument1.IsRequired);
            Assert.AreEqual(expectedOptionName2, argument2.Name);
            Assert.AreEqual(expectedOptionDescription2, argument2.Description);
            Assert.AreEqual(actionResult2, expectedActionResult2);
            Assert.IsFalse(argument2.IsRequired);
        }

        public class TestInlineConsoleCliCommand : InlineConsoleCliCommand
        {
            public TestInlineConsoleCliCommand(string commandName, string description)
                : base(commandName, description)
            {
            }

            public new void HasOption<T>(string name, string description, Action<T> action)
            {
                base.HasOption(name, description, action);
            }

            public new void HasRequiredOption<T>(string name, string description, Action<T> action)
            {
                base.HasRequiredOption(name, description, action);
            }

            protected override void Execute()
            {
                throw new NotImplementedException();
            }
        }
    }
}
