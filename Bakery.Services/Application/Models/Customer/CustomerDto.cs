using System;

namespace Bakery.Services.Application.Models.Customer
{
    public class CustomerDto
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDescription { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}