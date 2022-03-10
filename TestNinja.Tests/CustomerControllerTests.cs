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
    public class CustomerControllerTests
    {
        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            var customer = new CustomerController();

            var result = customer.GetCustomer(0);

            //Type of means that is NotFound Object
            Assert.That(result, Is.TypeOf<NotFound>());

            //InstanceOf means that is NotFound object or one of its derivatives
            Assert.That(result, Is.InstanceOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IdIsDifferentToZero_ReturnOk()
        {
            var customer = new CustomerController();

            var result = customer.GetCustomer(1);

            //Type of means that is Ok Object
            Assert.That(result, Is.TypeOf<Ok>());

            //InstanceOf means that is Ok object or one of its derivatives
            Assert.That(result, Is.InstanceOf<Ok>());
        }
    }
}
