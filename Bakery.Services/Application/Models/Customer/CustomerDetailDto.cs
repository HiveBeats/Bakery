using System;
using System.Collections.Generic;
using Bakery.Services.Application.Models.CustomerAddress;
using Bakery.Services.Application.Models.CustomerDiscount;

namespace Bakery.Services.Application.Models.Customer
{
    public class CustomerDetailDto
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDescription { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        
        public IEnumerable<CustomerAddressDto> Addresses { get; set; }
        public IEnumerable<CustomerDiscountDto> Discounts { get; set; }
    }
}