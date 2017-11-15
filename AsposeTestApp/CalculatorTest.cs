using NUnit.Framework;

namespace AsposeTestApp
{
    [TestFixture]
    public class CalculatorTest
    {
        [TestCase]
        public void AddTest()
        {
            Calculator helper = new Calculator();
            decimal result = helper.Addition(20, 10);
            Assert.AreEqual(30, result);
        }

        [TestCase]
        public void SubtractTest()
        {
            Calculator helper = new Calculator();
            decimal result = helper.Subtraction(20, 10);
            Assert.AreEqual(10, result);
        }
    }
}
