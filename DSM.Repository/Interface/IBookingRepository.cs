using DSM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSM.Repository.Interface
{
    public interface IBookingRepository
    {
        void AddBooking(BookingRequest booking);

        List<BookingRequest> GetAllBookings();

        BookingRequest GetById(int id);

        void UpdateStatus(int id, string status);
    
}
}
