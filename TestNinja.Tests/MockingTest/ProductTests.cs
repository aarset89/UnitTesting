using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.Tests.MockingTest
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void GetPrice_IsGoldCustomer_Return70PorcentValue()
        {
            var product = new Product { ListPrice = 100};

            var result = product.GetPrice(new Customer { IsGold = true });

            Assert.That(result, Is.EqualTo(70));
        }
        
        [Test]
        public void GetPrice_IsGoldCustomer_Return70PorcentValue2()
        {
            var product = new Product();
            product.ListPrice = 100;

            var customer = new Mock<ICustomer>();

            customer.Setup(c => c.IsGold).Returns(true);
            

            var result = product.GetPrice(customer.Object);

            Assert.That(result, Is.EqualTo(70));
        }
    }
}
