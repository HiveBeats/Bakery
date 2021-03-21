using System.Collections.Generic;
using System.Threading.Tasks;
using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.CustomerAddress;

namespace Bakery.Services.Domain.Address
{
    public interface IAddressService
    {
        Task<Result<IEnumerable<NearestAddressDto>>> GetNearest(Location location, float distance);
        Task<Result<AddressDto>> Create(Core.Entities.Customer customer, CreateCustomerAddress request);
    }
}