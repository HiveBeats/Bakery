using Bakery.Services.Application.Models.CustomerAddress;

namespace Bakery.Services.Application.Models.Customer
{
    public class AddressableCustomerDto : CustomerDto
    {
        public NearestAddressDto Address { get; set; }
    }
}