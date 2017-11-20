using NUnit.Framework;

namespace AsposeTestApp
{
    [TestFixture]
    class SpecificCalculatorTest
    {
        [TestCase]
        public void MultiplicationTest()
        {
            Calculator helper = new Calculator();
            decimal result = helper.Multiplication(2, 10);
            Assert.AreEqual(20, result);
        }

        [TestCase]
        public void DivisionTest()
        {
            Calculator helper = new Calculator();
            decimal result = helper.Division(20, 10);
            Assert.AreEqual(2, result);
        }
    }
}
