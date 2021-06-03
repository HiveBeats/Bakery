using System;
using System.Collections.Generic;

namespace Bakery.Core.Entities
{
    public class CustomerDiscount
    {
        public CustomerDiscount()
        {
            DiscountId = Guid.NewGuid().ToString();
            DiscountTime = new HashSet<DiscountTime>();
        }

        public string CustomerId { get; set; }
        public string DiscountId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<DiscountTime> DiscountTime { get; set; }
        
        public void Close()
        {
            DateEnd = DateTime.UtcNow;
        }
    }
}
