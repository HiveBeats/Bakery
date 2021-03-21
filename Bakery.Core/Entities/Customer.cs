using System;
using System.Collections.Generic;

namespace Bakery.Core.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerAddress = new HashSet<CustomerAddress>();
            CustomerDiscount = new HashSet<CustomerDiscount>();
        }

        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDescription { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public virtual ICollection<CustomerAddress> CustomerAddress { get; set; }
        public virtual ICollection<CustomerDiscount> CustomerDiscount { get; set; }
    }
}
