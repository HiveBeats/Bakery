using System;
using System.Collections.Generic;

namespace Bakery.Core.Entities
{
    public class CustomerAddress
    {
        public CustomerAddress(Customer customer, float latitude, float longitude, string name)
        {
            Customer = customer;
            Latitude = latitude;
            Longitude = longitude;
            AddressName = name;
            DateStart = DateTime.UtcNow;
        }
        
        protected CustomerAddress()
        {
            
        }
        public long CustomerId { get; set; }
        public long AddressId { get; set; }
        public string AddressName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public virtual Customer Customer { get; set; }
        
        
        public void Close()
        {
            DateEnd = DateTime.UtcNow;
        }
    }
}
