using CodeConcepts.FrameworkExtensions.ObjectExtensions;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;

namespace CodeConcepts.FrameworkExtensions.UnitTests
{
    [TestFixture]
    public class DynamicExtensionsUnitTests
    {
        [Test]
        public void WhenIConvertToDynamicTheSameObjectIsReturnedButMarkedAsDynamic()
        {
            var expectedMessage = "Operator '+' cannot be applied to operands of type 'int' and 'object'";
            var obj = new object();

            var result = obj.AsDynamic();
            
            try
            {
                // ReSharper disable once UnusedVariable
                var another = 1 + result;
                Assert.Fail("Should not reach");
            }
            catch (RuntimeBinderException runtimeBinderException)
            {
                Assert.AreSame(obj, result);
                Assert.AreEqual(expectedMessage, runtimeBinderException.Message);
            }
        }
    }
}