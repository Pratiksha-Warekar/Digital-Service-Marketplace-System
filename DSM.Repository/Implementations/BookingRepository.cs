using DSM.Models.Entities;
using DSM.Repository.Data;
using DSM.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace DSM.Repository.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }


       
        public void AddBooking(BookingRequest booking)
        {
            _context.BookingRequests.Add(booking);
            _context.SaveChanges();
        }

       
        public List<BookingRequest> GetAllBookings()
        {
            return _context.BookingRequests.ToList();
        }

        
        public BookingRequest GetById(int id)
        {
            return _context.BookingRequests.FirstOrDefault(x => x.BookingRequestId == id);
        }

       
        public void UpdateStatus(int id, string status)
        {
            var data = _context.BookingRequests.FirstOrDefault(x => x.BookingRequestId == id);

            if (data != null)
            {
                data.Status = status;
                _context.SaveChanges();
            }
        }
    }
}