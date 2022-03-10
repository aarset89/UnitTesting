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
    public class BookingHelperTests
    {
        private Booking _existingBooking;
        private Mock<IBookingRepository> _bookingRepository;

        [SetUp]
        public void SetUp()
        {
            _existingBooking = new Booking
            {
                Id = 1,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"

            };

            _bookingRepository = new Mock<IBookingRepository>();
            _bookingRepository.Setup(r => r.GetActiveBookings(2)).Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable());
        }

        [Test]
        public void OverlappingBookingsExist_WhenBookingIsCancelled_ReturnEmptyString()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = Before(_existingBooking.ArrivalDate,2),
                DepartureDate = Before(_existingBooking.ArrivalDate),
                Reference = "b"
            }, _bookingRepository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_WhenBookingIsOverlaped_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate),
                Reference = "b"
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }
        
        
        [Test]
        public void OverlappingBookingsExist_WhenBookingStartsBeforesAndFinishesAfter_ReturnReferenceString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
                Reference = "b"
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_WhenBookingStartsAndEndInTheMiddleOfExistingBooking_ReturnReferenceString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate),
                Reference = "b"
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_WhenBookingStartsAndEndInafterExistingBooking_ReturnReferenceString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate,3),
                Reference = "b"
            }, _bookingRepository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_WhenBookingIsCncelledAndOverlaps_ReturnReferenceString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate),
                Reference = "b",
                Status= "Cancelled"
            }, _bookingRepository.Object);

            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days = 1) => dateTime.AddDays(-days);
        private DateTime After(DateTime dateTime, int days = 1) => dateTime.AddDays(days);

        private DateTime ArriveOn(int year, int month, int day) => new DateTime(year, month, day, 14, 0, 0);
        private DateTime DepartOn(int year, int month, int day) => new DateTime(year, month, day, 14, 0, 0);
    }
}
