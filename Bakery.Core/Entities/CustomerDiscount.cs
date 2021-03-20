using System;
using System.Collections.Generic;

namespace Bakery.Core.Entities
{
    public class CustomerDiscount
    {
        public CustomerDiscount()
        {
            DiscountTime = new HashSet<DiscountTime>();
        }

        public long CustomerId { get; set; }
        public long DiscountId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<DiscountTime> DiscountTime { get; set; }
    }
}
