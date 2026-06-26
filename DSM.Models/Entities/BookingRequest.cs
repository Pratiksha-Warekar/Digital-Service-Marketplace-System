using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSM.Models.Entities
{
    public class BookingRequest
    {
        public int BookingRequestId { get; set; }
       
        public int UserId { get; set; }

        public string? UserName { get; set; }

        public int ServiceId { get; set; }

        public string? ServiceName { get; set; }

        public string? ProviderName { get; set; }

        public decimal Price { get; set; }

        public string? PhotoURL { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public DateTime BookingDate { get; set; }
            = DateTime.Now;

        public string Status { get; set; }
            = "Pending";
    }
}