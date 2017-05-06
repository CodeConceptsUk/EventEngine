using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CliConsole.UnitTests
{
    [TestClass]
    public class CommandArgumentUnitTests
    {
        [TestMethod]
        public void WhenICreateACommandArgumentItIsCreatedCorrectly()
        {
            const int expectedActionValue = 12345;
            const bool expectedIsRequired = true;
            var expectedArgumentName = Guid.NewGuid().ToString();
            var expectedArgumentDescription = Guid.NewGuid().ToString();
            var actionValue = 0;

            var target = new CommandArgument<int>(expectedArgumentName, expectedArgumentDescription, i => actionValue = i, expectedIsRequired);
            target.Action.Invoke(expectedActionValue);

            Assert.AreEqual(expectedArgumentName, target.Name);
            Assert.AreEqual(expectedArgumentDescription, target.Description);
            Assert.AreEqual(expectedIsRequired, target.IsRequired);
            Assert.AreEqual(expectedActionValue, actionValue);
            Assert.IsInstanceOfType(target, typeof(CommandArgument<int>));
            Assert.IsInstanceOfType(target, typeof(CommandArgument));
        }
    }
}
