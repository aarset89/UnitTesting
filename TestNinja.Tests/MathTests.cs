using NUnit.Framework;
using System.Linq;
using TestNinja.Fundamentals;

namespace TestNinja.Tests
{
    [TestFixture]
    public class Tests
    {

        private Math _math;
        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        [Test]
        [TestCase(1,2,3)]
        [TestCase(2,2,4)]
        [TestCase(1,3,4)]
        public void Add_WhenCalled_ReturnSumOfArguments(int a, int b, int expectedResult)
        {
            var result = _math.Add(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
        
        [Test]
        [TestCase(2,1,2)]
        [TestCase(1,2,2)]
        [TestCase(2,2,2)]
        public void Max_FirstArgumentIsGreater_ReturnTheFirstArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGraterThanZero_ReturnOddNumbresUpToLimit()
        {
            var result = _math.GetOddNumbers(5);

            Assert.That(result, Is.Not.Empty);

            Assert.That(result.Count(), Is.EqualTo(3));

            Assert.That(result, Does.Contain(1));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));

            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.Unique);

        }
    }
}