using System;

namespace Bakery.Services.Application.Models.CustomerAddress
{
    public class CustomerAddressDto
    {
        public long AddressId { get; set; }
        public string AddressName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}