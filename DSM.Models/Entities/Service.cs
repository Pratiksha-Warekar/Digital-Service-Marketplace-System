using System;

namespace DSM.Models.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }

        public string? ServiceName { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public int ProviderId { get; set; }

        public string? ProviderName { get; set; }

        public string? Address { get; set; }

        public string? PhotoURL { get; set; }

        public string Status { get; set; } = "Pending";
    }
}