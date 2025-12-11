using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PETest1.Models
{
    public class FlightBookingRespo
    {
        private readonly Petest1Context _context;

        public FlightBookingRespo()  // Khởi tạo _context trong constructor
        {
            _context = new Petest1Context();
        }

        public List<Table> GetAllFlightBooking()
        {
            return _context.Tables.ToList();
        }

        public void Add(Table flightBooking)
        {
            _context.Tables.Add(flightBooking);
            _context.SaveChanges();
        }

        public void Update(Table flightBooking)
        {
            _context.Tables.Update(flightBooking);
            _context.SaveChanges();
        }

        public void Delete(Table flightBooking)
        {
            _context.Tables.Remove(flightBooking);
            _context.SaveChanges();
        }

    }
}
