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
    public class FizzBuzzTests
    {
        [Test]
        [TestCase(1,"1")]
        [TestCase(2,"2")]
        [TestCase(3,"Fizz")]
        [TestCase(4,"4")]
        [TestCase(5,"Buzz")]
        [TestCase(6,"Fizz")]
        [TestCase(7,"7")]
        [TestCase(14,"14")]
        [TestCase(15,"FizzBuzz")]
        [TestCase(30,"FizzBuzz")]
        public void GetOutput_WhenCalled_ReturnString(int num, string expectedReturn)
        {
            var result = FizzBuzz.GetOutput(num);

            Assert.That(result, Is.EqualTo(expectedReturn));
            
        }
    }
}
