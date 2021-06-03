using System;
using System.Collections.Generic;

namespace Bakery.Services.Application.Models.CustomerDiscount
{
    public class CustomerDiscountDto
    {
        public string DiscountId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        
        public IEnumerable<DiscountTimeDto> DiscountTimes { get; set; }
    }
}