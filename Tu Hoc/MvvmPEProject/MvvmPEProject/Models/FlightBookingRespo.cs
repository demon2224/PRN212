using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmPEProject.Models
{
    public class FlightBookingRespo
    {
        private readonly LongHoPeContext _context;

        public FlightBookingRespo()  // Khởi tạo _context trong constructor
        {
            _context = new LongHoPeContext();
        }

        public List<FlightBooking> GetAllFlightBooking()
        {
            return _context.FlightBookings.ToList();
        }

        public void Add(FlightBooking flightBooking)
        {
            _context.FlightBookings.Add(flightBooking);
            _context.SaveChanges();
        }

        public void Update(FlightBooking flightBooking)
        {
            _context.FlightBookings.Update(flightBooking);
            _context.SaveChanges();
        }

        public void Delete(FlightBooking flightBooking)
        {
            _context.FlightBookings.Remove(flightBooking);
            _context.SaveChanges();
        }

    }
}
