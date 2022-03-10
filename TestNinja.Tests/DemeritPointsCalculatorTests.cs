using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.Tests
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {

        private DemeritPointsCalculator _dem;

        [SetUp]
        public void SetUp()
        {
            _dem = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(-5)]
        [TestCase(305)]
        public void CalculateDemeritPoints_SpeedIsLowerThanZeroOrGreaterThan300_ThrowsArgumentOutOfRangeException(int speed)
        {
            Assert.That(()=> _dem.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(60, 0)]
        [TestCase(65, 0)]
        [TestCase(66, 0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoint(int speed, int demeritPoint)
        {
            var result = _dem.CalculateDemeritPoints(60);

            Assert.That(result, Is.EqualTo(0));
        }
    }
}
